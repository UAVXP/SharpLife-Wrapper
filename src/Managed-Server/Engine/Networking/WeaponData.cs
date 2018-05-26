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

namespace Server.Engine.Networking
{
    public sealed unsafe class WeaponData
    {
        internal struct Native
        {
            internal int m_iId;
            internal int m_iClip;

            internal float m_flNextPrimaryAttack;
            internal float m_flNextSecondaryAttack;
            internal float m_flTimeWeaponIdle;

            internal int m_fInReload;
            internal int m_fInSpecialReload;
            internal float m_flNextReload;
            internal float m_flPumpTime;
            internal float m_fReloadTime;

            internal float m_fAimedDamage;
            internal float m_fNextAimBonus;
            internal int m_fInZoom;
            internal int m_iWeaponState;

            internal int iuser1;
            internal int iuser2;
            internal int iuser3;
            internal int iuser4;
            internal float fuser1;
            internal float fuser2;
            internal float fuser3;
            internal float fuser4;
        }

        internal Native* Data { get; }

        internal WeaponData(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public int Id
        {
            get => Data->m_iId;
            set => Data->m_iId = value;
        }

        public int Clip
        {
            get => Data->m_iClip;
            set => Data->m_iClip = value;
        }

        public float NextPrimaryAttack
        {
            get => Data->m_flNextPrimaryAttack;
            set => Data->m_flNextPrimaryAttack = value;
        }

        public float NextSecondaryAttack
        {
            get => Data->m_flNextSecondaryAttack;
            set => Data->m_flNextSecondaryAttack = value;
        }

        public float TimeWeaponIdle
        {
            get => Data->m_flTimeWeaponIdle;
            set => Data->m_flTimeWeaponIdle = value;
        }

        //These 2 are ints to allow networking of more data
        public int InReload
        {
            get => Data->m_fInReload;
            set => Data->m_fInReload = value;
        }

        public int InSpecialReload
        {
            get => Data->m_fInSpecialReload;
            set => Data->m_fInSpecialReload = value;
        }

        public float NextReload
        {
            get => Data->m_flNextReload;
            set => Data->m_flNextReload = value;
        }

        public float PumpTime
        {
            get => Data->m_flPumpTime;
            set => Data->m_flPumpTime = value;
        }

        public float ReloadTime
        {
            get => Data->m_fReloadTime;
            set => Data->m_fReloadTime = value;
        }

        public float AimedDamage
        {
            get => Data->m_fAimedDamage;
            set => Data->m_fAimedDamage = value;
        }

        public float NextAimBonus
        {
            get => Data->m_fNextAimBonus;
            set => Data->m_fNextAimBonus = value;
        }

        public int InZoom
        {
            get => Data->m_fInZoom;
            set => Data->m_fInZoom = value;
        }

        public int WeaponState
        {
            get => Data->m_iWeaponState;
            set => Data->m_iWeaponState = value;
        }

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
    }
}
