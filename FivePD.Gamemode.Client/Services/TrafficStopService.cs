// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.Common;
using FivePD.Common.Extensions;
using FivePD.Common.Models;
using FivePD.Gamemode.Client.Extensions;
using FivePD.Gamemode.Client.Interfaces;
using FivePD.Gamemode.Client.Utilities;
using Newtonsoft.Json;
using Cfx = CitizenFX.Core.Native;

#pragma warning disable CS4014
#pragma warning disable S3241

namespace FivePD.Gamemode.Client.Services
{
    /// <inheritdoc />
    public class TrafficStopService : ITrafficStopService
    {
        private readonly IKeybindService _keybindService;
        private readonly IPoliceEventService _policeEventService;
        private readonly INotificationService _notificationService;
        private readonly IVehicleService _vehicleService;
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficStopService"/> class.
        /// </summary>
        /// <param name="keybindService">The <see cref="IKeybindService"/> to use.</param>
        /// <param name="policeEventService">The <see cref="IPoliceEventService"/> to use.</param>
        /// <param name="notificationService">The <see cref="INotificationService"/> to use.</param>
        /// <param name="vehicleService">The <see cref="IVehicleService"/> to use.</param>
        /// <param name="localizationService">The <see cref="ILocalizationService"/> to use.</param>
        public TrafficStopService(IKeybindService keybindService, IPoliceEventService policeEventService, INotificationService notificationService, IVehicleService vehicleService, ILocalizationService localizationService)
        {
            this._keybindService = keybindService;
            this._policeEventService = policeEventService;
            this._notificationService = notificationService;
            this._vehicleService = vehicleService;
            this._localizationService = localizationService;
        }

        /// <inheritdoc />
        public void RegisterKeybinds()
        {
            this._keybindService.RegisterHoldableKeybind(
                KeybindIdentifiers.TrafficStopToggle,
                string.Empty,
                "LSHIFT",
                1000,
                () =>
                {
                    if (this._policeEventService.IsAttachedToEvent() && this._policeEventService.GetCurrentEvent() is TrafficStop && this._policeEventService.IsEventMainPlayerEqualsToPlayer())
                    {
                        this.Cancel();
                    }
                    else
                    {
                        BaseScript.TriggerServerEvent(Events.TrafficStop.IsVehicleAttached, this.GetCurrentTrafficStop()?.StoppedVehicleNetworkId ?? 0, new Action<bool>(isAttached =>
                        {
                            if (!isAttached)
                            {
                                this.Initiate();
                            }
                        }));
                    }
                },
                null,
                () =>
                {
                    // TODO: check if player is sitting in a police vehicle
                    return Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle.Driver == Game.PlayerPed;
                });
        }

        /// <summary>
        /// Initiate a traffic stop.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task Initiate()
        {
            var stoppedVehicle = this.FindClosestVehicleInFrontOfPlayer();

            // TODO: Is this range check needed?
            if (stoppedVehicle is null || !stoppedVehicle.IsInRangeOf(Game.PlayerPed.Position, 25f))
            {
                return;
            }

            var stoppedVehicleBlip = stoppedVehicle.CreateBlip(BlipSprite.PersonalVehicleCar, FBlipColor.LemonGreen, this._localizationService.CurrentLocalization.TrafficStop.StoppedVehicle);
            stoppedVehicleBlip.IsFlashing = true;
            this._vehicleService.GenerateVehicle(stoppedVehicle);

            var trafficStop = new TrafficStop(Game.PlayerPed.NetworkId, Game.PlayerPed.Position, stoppedVehicle)
            {
                Title = "Traffic stop",
                Address = Game.PlayerPed.Position.GetStreetName(),
            };
            this._policeEventService.SetCurrentEvent(trafficStop);
            BaseScript.TriggerServerEvent(Events.TrafficStop.Initiate, JsonConvert.SerializeObject(trafficStop));

            this._notificationService.ShowTooltip(
                this._localizationService.CurrentLocalization.TrafficStop.Initiate.ReplaceParams(
                    new Dictionary<string, string>
                    {
                        { "initiate", new GtaStyleText().Green().Build() },
                    },
                    new GtaStyleText().Reset().Build()),
                true);
            while (Game.PlayerPed.IsInVehicle() && !Game.PlayerPed.CurrentVehicle.IsSirenActive)
            {
                if (!this._policeEventService.IsAttachedToEvent())
                {
                    return;
                }

                await BaseScript.Delay(50);
            }

            stoppedVehicleBlip.IsFlashing = false;
            this._notificationService.ShowNotification(this._localizationService.CurrentLocalization.TrafficStop.WaitUntilStop);

            // TODO: Is this persistency thing needed with OneSync?
            stoppedVehicle.Driver.IsPersistence(true);
            foreach (var ped in stoppedVehicle.Occupants)
            {
                ped.IsPersistence(true);
            }

            stoppedVehicle.Driver.KeepTask(true);
            stoppedVehicle.Driver.Task.ClearAll();

            var stopPoint = default(Vector3);
            var stoppedVehicleForwardVector = stoppedVehicle.Position + (stoppedVehicle.ForwardVector * 40f);
            Cfx.API.GetRoadSidePointWithHeading(stoppedVehicleForwardVector.X, stoppedVehicleForwardVector.Y, stoppedVehicleForwardVector.Z, stoppedVehicle.Heading, ref stopPoint);

            stoppedVehicle.Driver.Task.DriveTo(stoppedVehicle, stopPoint, 2f, 10f);
            while (!stoppedVehicle.IsInRangeOf(stopPoint, 2f))
            {
                await BaseScript.Delay(50);
            }

            stoppedVehicle.Driver.Task.StandStill(-1);
            this._notificationService.ShowNotification(this._localizationService.CurrentLocalization.TrafficStop.Proceed);
        }

        /// <summary>
        /// Cancels the traffic stop of the player.
        /// </summary>
        private void Cancel()
        {
            var trafficStop = this.GetCurrentTrafficStop();
            if (trafficStop is null)
            {
                return;
            }

            this._notificationService.ShowTooltip(this._localizationService.CurrentLocalization.TrafficStop.Canceled, true);
            trafficStop.StoppedVehicle.RemoveBlips();
            BaseScript.TriggerServerEvent(Events.TrafficStop.Cancel, this._policeEventService.GetCurrentEvent().Id.ToString());
            this._policeEventService.SetCurrentEvent(null);
        }

        private Vehicle FindClosestVehicleInFrontOfPlayer()
        {
            var playerPedForwardVector = Game.PlayerPed.Position + (Game.PlayerPed.ForwardVector * 12f);
            var closestVehicleInFrontOfPlayer = World
                .GetAllVehicles()
                .Where(vehicle => vehicle.Driver.Exists())
                .Where(vehicle => !vehicle.Driver.IsPlayer)
                .Where(vehicle => vehicle.IsInRangeOf(playerPedForwardVector, 8f))
                .OrderBy(vehicle => World.GetDistance(playerPedForwardVector, vehicle.Position))
                .FirstOrDefault();

            if (closestVehicleInFrontOfPlayer is null)
            {
                return null;
            }

            if (!closestVehicleInFrontOfPlayer.IsAttached())
            {
                return closestVehicleInFrontOfPlayer;
            }

            var entityAttachedTo = closestVehicleInFrontOfPlayer.GetEntityAttachedTo();
            if (!entityAttachedTo.Model.IsVehicle)
            {
                return null;
            }

            var towingVehicle = (Vehicle)entityAttachedTo;
            return towingVehicle.Driver.IsPlayer ? null : towingVehicle;
        }

        private TrafficStop GetCurrentTrafficStop()
        {
            return (TrafficStop)this._policeEventService.GetCurrentEvent();
        }
    }
}