// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using CitizenFX.Core;
using FivePD.Gamemode.Client.Services;
using FivePD.Gamemode.Client.Utilities;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Client.Extensions
{
    /// <summary>
    /// Holds extension methods for CitizenFX.Core.Entity.
    /// </summary>
    public static class EntityExtensions
    {
        private static readonly TempDataStorageService TempDataStorageService = new TempDataStorageService();

        private static readonly uint[] UnsupportedPedHashes =
        {
            (uint)PedHash.Humpback,
            (uint)PedHash.Dolphin,
            (uint)PedHash.KillerWhale,
            (uint)PedHash.Fish,
            (uint)PedHash.HammerShark,
            (uint)PedHash.TigerShark,
            (uint)PedHash.Boar,
            (uint)PedHash.Cat,
            (uint)PedHash.ChickenHawk,
            (uint)PedHash.Chimp,
            (uint)PedHash.Coyote,
            (uint)PedHash.Cow,
            (uint)PedHash.Deer,
            (uint)PedHash.Pig,
            (uint)PedHash.Rabbit,
            (uint)PedHash.Crow,
            (uint)PedHash.Cormorant,
            (uint)PedHash.Husky,
            (uint)PedHash.Rottweiler,
            (uint)PedHash.Pug,
            (uint)PedHash.Poodle,
            (uint)PedHash.Retriever,
            (uint)PedHash.Shepherd,
            (uint)PedHash.Seagull,
            (uint)PedHash.Pigeon,
            (uint)PedHash.MountainLion,
            (uint)PedHash.BradCadaverCutscene,
            (uint)PedHash.Chop,
            (uint)PedHash.Hen,
            (uint)PedHash.JohnnyKlebitz,
            (uint)PedHash.LamarDavisCutscene,
            (uint)PedHash.MagentaCutscene,
            (uint)PedHash.Marston01,
            (uint)PedHash.Misty01,
            (uint)PedHash.MovAlien01,
            (uint)PedHash.MoviePremFemaleCutscene,
            (uint)PedHash.MoviePremMaleCutscene,
            (uint)PedHash.MrsPhillipsCutscene,
            (uint)PedHash.MrKCutscene,
            (uint)PedHash.NataliaCutscene,
            (uint)PedHash.NigelCutscene,
            (uint)PedHash.NervousRonCutscene,
            (uint)PedHash.Niko01,
            (uint)PedHash.PaigeCutscene,
            (uint)PedHash.OscarCutscene,
            (uint)PedHash.OrtegaCutscene,
            (uint)PedHash.OrleansCutscene,
            (uint)PedHash.Orleans,
            (uint)PedHash.Pogo01,
            (uint)PedHash.Rat,
            (uint)PedHash.Rhesus,
            (uint)PedHash.Stingray,
            (uint)PedHash.SteveHainsCutscene,
            (uint)PedHash.Westy,
        };

        /// <summary>
        /// Initializes a blip for a given entity.
        /// </summary>
        /// <param name="entity">The entity to add the blip for.</param>
        /// <param name="sprite">The blip's sprite.</param>
        /// <param name="color">The blip's color.</param>
        /// <param name="text">The blip's title that'll be displayed in the map.</param>
        /// <returns>Returns the created blip.</returns>
        public static Blip CreateBlip(this Entity entity, BlipSprite sprite, FBlipColor color, string text)
        {
            var blip = entity.AttachBlip();
            blip.Sprite = sprite;
            blip.Color = (BlipColor)color;
            Cfx.API.BeginTextCommandSetBlipName("STRING");
            Cfx.API.AddTextComponentString(text);
            Cfx.API.EndTextCommandSetBlipName(blip.Handle);
            return blip;
        }

        /// <summary>
        /// Deletes all blips that are attached to the given entity.
        /// </summary>
        /// /// <param name="entity">The entity whose blips need to be deleted.</param>
        public static void RemoveBlips(this Entity entity)
        {
            foreach (var blip in entity.AttachedBlips)
            {
                blip.Delete();
            }
        }

        /// <summary>
        /// Used to determine if the given entity has a human hash.
        /// Should exclude all animal and mission peds.
        /// </summary>
        /// <param name="entity">The entity to run the check for.</param>
        /// <returns>Returns if the entity has a human hash.</returns>
        public static bool HasHumanHash(this Entity entity)
        {
            return Array.IndexOf(UnsupportedPedHashes, (uint)entity.Model.Hash) == -1;
        }

        /// <summary>
        /// Can be used to toggle the SetEntityAsMissionEntity() state of the given entity.
        /// </summary>
        /// <param name="entity">The entity to set its persistency for.</param>
        /// <param name="state">The state it should be set to. If set to true it won't be deleted by GTA ? .</param>
        public static void IsPersistence(this Entity entity, bool state)
        {
            Cfx.API.SetEntityAsMissionEntity(entity.Handle, state, state);
        }

        /// <summary>
        /// Get temporary replicated data off this <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">Entity to get data for.</param>
        /// <param name="key">Key to get.</param>
        /// <returns>Temporary replicated data.</returns>
        public static dynamic GetTempData(this Entity entity, string key) => TempDataStorageService.Get(entity, key);
    }
}