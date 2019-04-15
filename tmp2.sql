/*
--                Bullet 2
--Color family (print color on label color)

IF $SP-21844$ LIKE "%labels" AND A[373].Where("black%on%white").Values IS NOT NULL
THEN "White labels with black print make text easy to read    " 

ELSE IF $SP-21844$ LIKE "%labels" AND A[373].Where("white", "blue").Values.Count = 2
THEN "White labels with blue print make text easy to read    " 

ELSE IF $SP-21844$ LIKE "%labels" AND A[373].Values.Where("%clear%", "%transparent%") IS NOT NULL
THEN A[373].Values.First().Split().First().Postfix(" print on clear label for legibility").ToUpperFirstChar()

ELSE IF $SP-21844$ LIKE "%labels" AND A[373].Values IS NOT NULL AND A[373].Values.First().Split().Count > 1
THEN A[373].Values.First().Split().First().Postfix(" print on ").ToUpperFirstChar()_A[373].Values.First().Split().Skip(2).First().Postfix(" labels for easy identification")
--tape

ELSE IF $SP-21844$ LIKE "%tapes" AND A[373].Where("black%on%white").Values IS NOT NULL
THEN "Black print on a white tape creates an easy-to-read text" 

ELSE IF $SP-21844$ LIKE "%tapes" AND A[373].Where("black", "silver").Values.Count = 2
THEN "Black print on a silver tape creates an easy-to-read text" 

ELSE IF $SP-21844$ LIKE "%tapes" AND A[373].Where("black%on%blue").Values IS NOT NULL
THEN "Black print on a blue tape ensures high visibility on light-colored surfaces" 

ELSE IF $SP-21844$ LIKE "%tapes" AND A[373].Where("black%on%yellow").Values IS NOT NULL
THEN "Black print on yellow tape for easy identification" 

ELSE IF $SP-21844$ LIKE "%tapes" AND A[373].Values.Where("%clear%", "%transparent%") IS NOT NULL
THEN A[373].Values.First().Split().First().Postfix(" print on clear tape for legibility").ToUpperFirstChar()

ELSE IF $SP-21844$ LIKE "%tapes" AND A[373].Values IS NOT NULL AND A[373].Values.First().Split().Count > 1
THEN A[373].Values.First().Split().First().Postfix(" print on ").ToUpperFirstChar()_A[373].Values.First().Split().Skip(2).First().Postfix(" tape for easy identification")

ELSE IF A[373].Values IS NULL OR $SP-21844$ IS NULL OR A[373].Values.First().Split().Count < 2 THEN NULL
ELSE "@@";
*/