--Compatibility
IF MS.CompatibleProducts IS NOT NULL THEN MS.CompatibleProducts.Values.FlattenWithAnd().Shorten(75).Erase("[...]").Replace("Single-sided", "single-sided").Replace("Dual Sided", "dual- sided").Prefix("Compatible with ").Postfix(" printers").Replace(" "," ##")
ELSE IF A[4782].Values IS NOT NULL THEN A[4782].Values.FlattenWithAnd().Shorten(75).Erase("[...]").Prefix("Compatible with ").Postfix(" printers").Replace(" "," ##")
ELSE "@@";

--Printer color (Black, Blue, Black/Red, etc.)
IF A[494].Count = 1 AND A[4760].Value LIKE "%ribbon" AND A[494].Values.Where("%black") IS NOT NULL THEN
"Black printer ribbon provides crisp, cost-effective printing suitable for a commercial environment" 
ELSE IF A[494].Count = 1 AND A[494].Values.Where("color (%") IS NOT NULL THEN
"Gives full color prints" 
ELSE IF A[494].Count = 1 AND A[494].Values.Where("multicolor") IS NOT NULL THEN
"Gives multicolor prints" 
ELSE IF A[494].Count = 4 AND A[494].Values.Where("black") IS NOT NULL AND A[494].Values.Where("cyan") IS NOT NULL AND A[494].Values.Where("magenta") IS NOT NULL AND A[494].Values.Where("yellow") IS NOT NULL THEN
"Gives full-color prints" 
ELSE "@@";

--Dimensions

IF $SP-21130$ IS NOT NULL AND A[2165].Unit LIKE "mm" THEN "Dimensions: "_A[2165].Value.ExtractDecimals().Max().MultiplyBy(0.03937008).ToText("F2").RegexReplace("(?<=\.\d)0", "").Erase(".0")_"""W x "_$SP-21130$_"'L" 
ELSE IF $SP-21130$ IS NOT NULL AND A[2165].Unit LIKE "cm" THEN "Dimensions: "_A[2165].Value.ExtractDecimals().Max().MultiplyBy(0.3937008).ToText("F2").RegexReplace("(?<=\.\d)0", "").Erase(".0")_"""W x "_$SP-21130$_"'L" 
ELSE IF $SP-21130$ IS NOT NULL AND A[2165].Unit LIKE "m" THEN "Dimensions: "_A[2165].Value.ExtractDecimals().Max().MultiplyBy(39.37008).ToText("F2").RegexReplace("(?<=\.\d)0", "").Erase(".0")_"""W x "_$SP-21130$_"'L" 
ELSE IF $SP-21130$ IS NOT NULL THEN 
"Dimension: "_$SP-21130$_"'L" 
ELSE "@@";

--Pack size 
IF Request.Data["TX_UOM"] LIKE "Each" THEN "Includes one "_A[4760].Value.IfLike("printer fabric ribbon", "fabric printer ribbon")
ELSE IF Request.Data["TX_UOM"] LIKE "%/%/%" THEN throw new MissingProductDataException("check TX_UOM")
ELSE IF Request.Data["TX_UOM"] LIKE "%/%" THEN Request.Data["TX_UOM"].Split("/").First().Postfix(" ribbons per ")_Request.Data["TX_UOM"].Split("/").Last().ToLower()
ELSE IF Request.Data["TX_UOM"] IS NOT NULL THEN throw new MissingProductDataException("check TX_UOM")
ELSE "@@";

--[Additional Bullet]

--https://www.staples.com/zebra-true-colours-ix-ribbon-cartridge-ymcko-200-cards/product_IM1GT0174
IF A[4785].Value IN ("% images","% pages") THEN "Prints "_A[4785].Value_" to maximize productivity and savings";

--example: Prints up to 8 million characters
--Prints up to 8 million characters for fewer ribbon changes
IF A[4785].Value LIKE "%characters" THEN A[4785].Value.Prefix("Prints up to ").Postfix(" for fewer ribbon changes");

--[Type of Product/Use]
--example: Epson ribbon cartridge for thermal transfer printing
IF A[4760].Value LIKE "re-inking ribbon" THEN
"Re-ink printer ribbon for adding ink to compatible printers";

--Printing Technology
IF A[4782].Count = 1 AND MS.CompatibleProducts IS NOT NULL THEN
A[4782].Values.Flatten().ToUpperFirstChar().Postfix(" print technology offers high-quality prints");

IF A[4760].Value LIKE "%with cleaning roller" THEN 
"Comes with cleaning roller";

--remanufactured
IF A[5312].Value LIKE "remanufactured" THEN
"Remanufactured to reduce environmental impact";

--warranty
IF A[430].Value LIKE "% warranty" THEN
A[430].Value.Replace(" months warranty","-month").Replace(" month warranty","-month").Replace(" years warranty","-year").Replace(" year warranty","-year").Replace(" days warranty","-day").Replace(" day warranty","-day").Postfix(" manufacturer limited warranty")
ELSE "@@";