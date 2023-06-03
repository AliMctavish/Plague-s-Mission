﻿

namespace gravityProject
{
    internal class Maps
    {
        public string[] LoadLevel(int level)
        {
            //  % symbol represent groundBase;
            //  $ symbol represent ground3;
            //  # symbol represent ground1;
            //  . symbol represent empty space;
            //  ? symbol represent chest;
            //  @ symbol represent coins;
            //  ! note ! you should not left a blank space without . symbol , and dont ask why :p
            if (level == 1)
            {
                string[] map = {",.........................",
                                "..........................",
                                "..........................",
                                "..s......|!@@@@?|....|!@@|",
                                "....h.....x$$$$yH....hx$$y",
                                "$$$$$$y.........H..x$$%%%%",
                                "%%%%%%%y|!|.....H+x%%%%%%%",
                                "%%%%%%%%$y...+.@x$%%%%%%%%",
                                "%%%%%%%%%%y..x$$%%%%%%%%%%",
                                "%%%%%%%%%%%....%%%%%%%%%%%",
                                "%%%%%%%%%%%^^^^%%%%%%%%%%%",
                                "%%%%%%%%%%%%%%%%%%%%%%%%%%",
                                "%%%%%%%%%%%%%%%%%%%%%%%%%%",
                                "%%%%%%%%%%%%%%%%%%%%%%%%%%"};
                return map;
            }
            if (level == 2)
            {
                string[] map = {",s........................",
                                "..@@@.....................",
                                "$$$$$y....................",
                                ".................?...|!@+|",
                                "..@@@...Hx$$$$$$$$y...x$$y",
                                "$$$$$y..H.................",
                                ".....%@@H@@@@.............",
                                ".....%$$$$$$$$$yH...h.....",
                                "...........%....H..x$$$$$$",
                                "..-.......@%?...H.x%%.....",
                                "..........x%y...Hx%%%.....",
                                "^^^^^^^^^^^%^^^^x%%%%^^^^^",
                                "%%%%%%%%%%%%%%%%%%%%%%%%%%"};
                return map;
            }
            if (level == 3)
            {
                string[] map = {",.........................",
                                "..........................",
                                "..........................",
                                "..........................",
                                "..s.......................",
                                ".............@@@..........",
                                "$$$$$$$$$$$$$yH@..........",
                                "..............H@..@@@.....",
                                "%.............H@@.........",
                                "%@@@.x$$$$$$$$$$y.........",
                                "$$$$$%.........%%^^^^.....",
                                "...............$$$$$$$$$$$",
                                ".........................."};
                return map;

            }
            if (level == 4)
            {
                string[] map = {",$%%......................",
                                "...s......................",
                                "..#########+#.+##.........",
                                "..xy..xy..xy..xy..........",
                                "..%%..%%..%%..%%..-.......",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%..........",
                                "..%%..%%..%%..%%.........." };
                return map;
            }
            if (level == 5)
            {
                string[] map = {",....................................",
                                ".....................................",
                                "....................................#",
                                "#%%%%y......................x$$y....%",
                                "%..............s....................%",
                                "%..+++x$$$$$$y....x$$$$$y....x$y....%",
                                "%..x$$%%%%y..x%%%%%%%%%..%%%%%%.....%",
                                "%...................................%",
                                "%...|.!@@@..!!!!!.+..@@..|...@......%",
                                "%$y%%%%%%%%%%%%%%%%%%%%%%%%%%%%.....%",
                                "%...................................%",
                                "%...................................%",
                                "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%",};
                return map;
            }
            if (level == 6)
            {
                string[] map = {",....................................",
                                ".....................................",
                                "....................................#",
                                "#..........................x$$y.....%",
                                "%.....|.!@...|.....@@@@@@...........%",
                                "%..+..x$$$$$$y....x$$$$$y....x$y....%",
                                "%..x$$%%%%y..x%%%%%%%%%..%%%%%%%%%%%%",
                                "%...................................%",
                                "%...|.!@@@.......+..@@..|...@.......%",
                                "%$$$$$$$$$$$$$$$$$$$$$$$y...........%",
                                "%...................................%",
                                "%...................................%",
                                "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%",
                                "%%%%..%%..%%..%%" };
                return map;
            }
            return null;
        }
    }
}
