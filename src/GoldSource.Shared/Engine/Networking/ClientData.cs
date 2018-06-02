/***
*
*	Copyright (c) 1996-2001, Valve LLC. All rights reserved.
*	
*	This product contains software technology licensed from Id 
*	Software, Inc. ("Id Technology").  Id Technology (c) 1996 Id Software, Inc. 
*	All Rights Reserved.
*
*   This source code contains proprietary and confidential information of
*   Valve LLC and its suppliers.  Access to this code is restricted to
*   persons who have executed a written SDK license with Valve.  Any access,
*   use or distribution of this code by or to any unlicensed person is illegal.
*
****/

using GoldSource.Mathlib;
using GoldSource.Shared.Entities;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GoldSource.Shared.Engine.Networking
{
    public sealed unsafe class ClientData
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Native
        {
            internal Vector origin;
            internal Vector velocity;

            internal int viewmodel;
            internal Vector punchangle;
            internal EntFlags flags;
            internal WaterLevel waterlevel;
            internal Contents watertype;
            internal Vector view_ofs;
            internal float health;

            internal int bInDuck;

            internal int weapons;

            internal int flTimeStepSound;
            internal int flDuckTime;
            internal int flSwimTime;
            internal int waterjumptime;

            internal float maxspeed;

            internal float fov;
            internal int weaponanim;

            internal int m_iId;
            internal int ammo_shells;
            internal int ammo_nails;
            internal int ammo_cells;
            internal int ammo_rockets;
            internal float m_flNextAttack;

            internal int tfstate;

            internal int pushmsec;

            internal DeadFlag deadflag;

            internal fixed byte physinfo[InternalConstants.MaxPhysInfoString];

            internal int iuser1;
            internal int iuser2;
            internal int iuser3;
            internal int iuser4;
            internal float fuser1;
            internal float fuser2;
            internal float fuser3;
            internal float fuser4;
            internal Vector vuser1;
            internal Vector vuser2;
            internal Vector vuser3;
            internal Vector vuser4;
        }

        internal Native* Data { get; }

        public ClientData(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public Vector Origin
        {
            get => Data->origin;
            set => Data->origin = value;
        }

        public Vector Velocity
        {
            get => Data->velocity;
            set => Data->velocity = value;
        }

        public int ViewModel
        {
            get => Data->viewmodel;
            set => Data->viewmodel = value;
        }

        public Vector PunchAngle
        {
            get => Data->punchangle;
            set => Data->punchangle = value;
        }

        public EntFlags Flags
        {
            get => Data->flags;
            set => Data->flags = value;
        }

        public WaterLevel WaterLevel
        {
            get => Data->waterlevel;
            set => Data->waterlevel = value;
        }

        public Contents WaterType
        {
            get => Data->watertype;
            set => Data->watertype = value;
        }

        public Vector ViewOffset
        {
            get => Data->view_ofs;
            set => Data->view_ofs = value;
        }

        public float Health
        {
            get => Data->health;
            set => Data->health = value;
        }

        public bool InDuck
        {
            get => 0 != Data->bInDuck;
            set => Data->bInDuck = value ? 1 : 0;
        }

        public int Weapons
        {
            get => Data->weapons;
            set => Data->weapons = value;
        }

        public int TimeStepSound
        {
            get => Data->flTimeStepSound;
            set => Data->flTimeStepSound = value;
        }

        public int DuckTime
        {
            get => Data->flDuckTime;
            set => Data->flDuckTime = value;
        }

        public int SwimTime
        {
            get => Data->flSwimTime;
            set => Data->flSwimTime = value;
        }

        public int WaterJumpTime
        {
            get => Data->waterjumptime;
            set => Data->waterjumptime = value;
        }

        public float MaxSpeed
        {
            get => Data->maxspeed;
            set => Data->maxspeed = value;
        }

        public float FieldOfView
        {
            get => Data->fov;
            set => Data->fov = value;
        }

        public int WeaponAnim
        {
            get => Data->weaponanim;
            set => Data->weaponanim = value;
        }

        public int WeaponId
        {
            get => Data->m_iId;
            set => Data->m_iId = value;
        }

        public int AmmoShells
        {
            get => Data->ammo_shells;
            set => Data->ammo_shells = value;
        }

        public int AmmoNails
        {
            get => Data->ammo_nails;
            set => Data->ammo_nails = value;
        }

        public int AmmoCells
        {
            get => Data->ammo_cells;
            set => Data->ammo_cells = value;
        }

        public int AmmoRockets
        {
            get => Data->ammo_rockets;
            set => Data->ammo_rockets = value;
        }

        public float NextAttack
        {
            get => Data->m_flNextAttack;
            set => Data->m_flNextAttack = value;
        }

        public int TFState
        {
            get => Data->tfstate;
            set => Data->tfstate = value;
        }

        public int PushMSec
        {
            get => Data->pushmsec;
            set => Data->pushmsec = value;
        }

        public DeadFlag DeadFlag
        {
            get => Data->deadflag;
            set => Data->deadflag = value;
        }

        public string PhysInfo
        {
            get => Marshal.PtrToStringUTF8(new IntPtr(Data->physinfo));

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                //Marshal it back to the fixed size buffer
                var bytes = Encoding.UTF8.GetBytes(value + '\0');
                Marshal.Copy(bytes, 0, new IntPtr(Data->physinfo), Math.Min(InternalConstants.MaxPhysInfoString, bytes.Length));
            }
        }

        /// <summary>
        /// Directly copy the unmanaged buffer into this one
        /// More efficient than doing a conversion to and from managed strings
        /// </summary>
        /// <param name="nativeMemory"></param>
        public void SetPhysInfo(IntPtr nativeMemory)
        {
            //No way to copy between unmanaged buffers
            var managedMemory = new byte[InternalConstants.MaxPhysInfoString];
            Marshal.Copy(nativeMemory, managedMemory, 0, InternalConstants.MaxPhysInfoString);
            Marshal.Copy(managedMemory, 0, new IntPtr(Data->physinfo), InternalConstants.MaxPhysInfoString);
        }

        //For mods
        public int UserInt1
        {
            get => Data->iuser1;
            set => Data->iuser1 = value;
        }

        public int UserInt2
        {
            get => Data->iuser2;
            set => Data->iuser2 = value;
        }

        public int UserInt3
        {
            get => Data->iuser3;
            set => Data->iuser3 = value;
        }

        public int UserInt4
        {
            get => Data->iuser4;
            set => Data->iuser4 = value;
        }

        public float UserFloat1
        {
            get => Data->fuser1;
            set => Data->fuser1 = value;
        }

        public float UserFloat2
        {
            get => Data->fuser2;
            set => Data->fuser2 = value;
        }

        public float UserFloat3
        {
            get => Data->fuser3;
            set => Data->fuser3 = value;
        }

        public float UserFloat4
        {
            get => Data->fuser4;
            set => Data->fuser4 = value;
        }

        public Vector UserVector1
        {
            get => Data->vuser1;
            set => Data->vuser1 = value;
        }

        public Vector UserVector2
        {
            get => Data->vuser2;
            set => Data->vuser2 = value;
        }

        public Vector UserVector3
        {
            get => Data->vuser3;
            set => Data->vuser3 = value;
        }

        public Vector UserVector4
        {
            get => Data->vuser4;
            set => Data->vuser4 = value;
        }
    }
}
