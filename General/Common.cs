using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsIn.General
{
    public class Common
    {
        public static string Data = @"3/7/20, 17:07 - User1: sample text
3/7/20, 17:07 - User2: 😂😂😂😂
3/7/20, 17:07 - User1: any data";

        public static HashSet<string> ExcludedKeyWords = new HashSet<string>(new string[] {
            "<Media","omitted>","في","من","ما","على","مش","https","اللي","هو","..","بس","دا","انا","ولا","يا","كان","ان","ايه","لو","دي","لا",")","كل","(",
            "عن","واحد","الناس","اصلا","أن","ۚ","بعد","انه","هي","او","غير","عليه","ممكن","باه","ۖ","انت","قبل","لما","مع","هنا","فيه","وهو","فِي",
            "إللي","ده","هوه","ﻣﻦ","حد","اه","-","دى","أيه","ﻫﻞ","كنت","و","ب","؟","اول", ".", "..", "...", "....", "فى", ",", "الي"
        });
    }
}
