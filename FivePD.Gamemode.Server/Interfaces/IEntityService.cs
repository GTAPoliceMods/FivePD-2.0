// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System.Threading.Tasks;
using CitizenFX.Core;

namespace FivePD.Gamemode.Server.Interfaces
{
     /**
     * <summary>Responsible for various entity management related tasks.</summary>
     */
    public interface IEntityService
    {
         /// <summary>
         /// Registers all entity management related network events.
         /// </summary>
         public void RegisterEvents();

         /**
         * <summary>Spawns a ped via RPC natives.</summary>
         * <param name="model">A ped model for the ped to use.</param>
         * <param name="location">A location where the ped will spawn.</param>
         * <param name="heading">The heading the ped will use.</param>
         * <returns>The spawned ped.</returns>
         */
         Task<Ped> SpawnPed(uint model, Vector3 location, float heading);

        /**
         * <summary>Spawns a vehicle directly on the server.</summary>
         * <param name="model">A ped model for the ped to use.</param>
         * <param name="location">A location where the ped will spawn.</param>
         * <param name="heading">The heading the ped will use.</param>
         * <param name="networkCallbackDelegate">The network callback delegate that'll be invoked with the spawned vehicle's network id.</param>
         * <returns>The spawned vehicle.</returns>
         */
         Task SpawnVehicle(uint model, Vector3 location, float heading, NetworkCallbackDelegate networkCallbackDelegate);

        /**
         * <summary>Spawns a prop (object) via RPC natives.</summary>
         * <param name="model">A model for the prop to use.</param>
         * <param name="location">A location where the prop will spawn.</param>
         * <returns>The spawned prop.</returns>
         */
         Task<Prop> SpawnProp(int model, Vector3 location);
    }
}