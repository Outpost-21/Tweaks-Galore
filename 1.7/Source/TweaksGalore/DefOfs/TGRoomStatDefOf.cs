using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
    [DefOf]
    public static class TGRoomStatDefOf
    {
        static TGRoomStatDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TGRoomStatDefOf));
        }

        public static RoomStatDef Space;
    }
}
