--<Type and Caffeine>
--$SP-17863$ - Caffeinated, Decaffeinated

--https://www.quill.com/green-mountain-dark-magic-coffee-96-count-##Keurig-k-cup-pods-dark-roast-96-carton-gmt4061ct/cbs/51820371.html?promoCode=&Effort_Code=901&Find_Number=04061CT&m=0&isSubscription=False --Gourmet extra bold dark roast coffee ##K-Cups for ##Keurig brewers.

IF $SP-17882$ LIKE "Coffee" AND $SP-17863$ LIKE "Caffeinated" 
THEN "Caffeinated coffee ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Coffee" AND $SP-17863$ LIKE "Decaffeinated" 
THEN "Decaffeinated coffee ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Tea" AND $SP-17863$ LIKE "Caffeinated" 
THEN "Caffeinated "_A[6721].Values.FlattenWithAnd()_" tea ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Tea" AND $SP-17863$ LIKE "Decaffeinated" 
THEN "Decaffeinated "_A[6721].Values.FlattenWithAnd()_" tea ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Hot Chocolate" AND $SP-17863$ LIKE "Caffeinated" 
THEN "Caffeinated hot chocolate ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Hot Chocolate" AND $SP-17863$ LIKE "Decaffeinated" 
THEN "Decaffeinated hot chocolate ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Seasonal" AND $SP-17863$ LIKE "Caffeinated" 
THEN "Caffeinated seasonal ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ LIKE "Seasonal" AND $SP-17863$ LIKE "Decaffeinated" 
THEN "Decaffeinated seasonal ##K-Cups for ##Keurig brewers" 
ELSE IF $SP-17882$ IS NULL THEN NULL 

ELSE IF $SP-17882$ IS NOT NULL
AND $SP-17863$ IS NOT NULL THEN
    $SP-17863$_" "_$SP-17882$_" ##K-Cups for ##Keurig brewers" 

ELSE IF $SP-17882$ IS NULL THEN NULL 
ELSE IF $SP-17882$ IS NOT NULL THEN
    $SP-17882$_"##K-Cups for ##Keurig brewers" 
ELSE "@@";

--<Roast and Flavor>

IF $SP-17870$ NOT IN (NULL, "Variety Pack")
AND $SP-17872$ LIKE "French roast" 
AND $SP-17882$ LIKE "Coffee" THEN
    $SP-17870$_" roast coffee with ##French-roast flavor" 

ELSE IF $SP-17870$ NOT IN (NULL, "Variety Pack")
AND $SP-17872$ NOT IN (NULL, "Variety Pack")
AND $SP-17882$ LIKE "Coffee" THEN
    $SP-17870$_" roast coffee with ##"_
    $cnet_flavoring$_" flavor" 

ELSE IF $SP-17882$ LIKE "Seasonal" AND A[6726].Values.Where("brown sugar").Where("%apple%") IS NOT NULL
THEN "Fresh, juicy goodness of sweet orchard apples with a hint of brown sugar" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) IS NOT NULL
AND A[6725].Values.Where("%vanila%", "%caramel%") IS NOT NULL AND $SP-17882$ LIKE "Coffee" THEN
    "A smooth, "_COALESCE($SP-17870$, A[6731].Value)_
    " roasted coffee with "_
    A[6725].Values.Where("%vanila%", "%caramel%").First().Prefix("##")_
    " flavor to satisfy sweet cravings" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) IS NOT NULL
AND $SP-17872$ IN ("Vanilla","Caramel") AND $SP-17882$ LIKE "Coffee" THEN
    "A smooth, "_COALESCE($SP-17870$, A[6731].Value)_
    " roasted coffee with ##"_
    $SP-17872$_
    " flavor to satisfy sweet cravings" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) IS NOT NULL
AND A[6725].Values IS NOT NULL AND $SP-17882$ LIKE "Coffee" THEN
     COALESCE($SP-17870$, A[6731].Value.ToUpperFirstChar())_" roast coffee with "_
     A[6725].Values.Prefix("##").FlattenWithAnd()_" flavor" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) IS NOT NULL
AND $SP-17872$ NOT IN (NULL, "Variety Pack") AND $SP-17882$ LIKE "Coffee" THEN
    A[6731].Value.ToUpperFirstChar()_"-roast coffee with ##"_
     $cnet_flavoring$_" flavor" 

ELSE IF A[6725].Values IS NOT NULL AND $SP-17882$ LIKE "Coffee" THEN
    "A great tasting Coffee with "_
    A[6725].Values.ToUpperFirstChar().Prefix("##").FlattenWithAnd()_
    " flavoring" 

ELSE IF $SP-17872$ NOT IN (NULL, "Variety Pack") AND $SP-17882$ LIKE "Coffee" THEN
    "A great tasting Coffee with ##"_
    $cnet_flavoring$_
    " flavoring" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) LIKE "dark" AND $SP-17882$ LIKE "Coffee" THEN
    "A great tasting dark roast coffee" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) LIKE "medium" AND $SP-17882$ LIKE "Coffee" THEN
    "The medium roast gives you a strong coffee taste that isn't overpowering" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) LIKE "Medium Dark" AND $SP-17882$ LIKE "Coffee" THEN
    "A hearty, full-bodied blend of medium and dark roasts" 

ELSE IF COALESCE($SP-17870$, A[6731].Value) LIKE "light" AND $SP-17882$ LIKE "Coffee" THEN
    "A lighter roasted coffee is buttery and sweet" 

ELSE IF $SP-17870$ NOT IN (NULL, "Variety Pack")
AND $SP-17882$ LIKE "Coffee" THEN
    $SP-17870$_" roast coffee" 

ELSE IF $SP-17872$ NOT IN (NULL, "Variety Pack")
AND $SP-17882$ LIKE "Tea" THEN
    $cnet_flavoring$_" tea" 

ELSE IF A[6721].Value IS NOT NULL
AND $SP-17882$ LIKE "Tea" THEN
    A[6721].Values.FlattenWithAnd().ToUpperFirstChar().Postfix(" tea") 

ELSE IF $SP-17872$ NOT IN (NULL, "Variety Pack", "%Chocolate%") AND $SP-17882$ LIKE "%Chocolate" THEN
    "Hot Chocolate with ##"_
    $cnet_flavoring$_" flavor" 

ELSE IF $SP-17872$ NOT IN (NULL, "Variety Pack") AND $SP-17882$ LIKE "Seasonal" THEN
    "Seasonal ##K-Cups with ##"_
    $cnet_flavoring$_" flavor" 

ELSE "@@";

--<Container Size>
--https://www.staples.com/product_465103    24 ##K-Cups per pack 

IF Request.Data["TX_UOM"] LIKE "Each" 
THEN "##K-Cups are sold individually" 
ELSE IF Request.Data["TX_UOM"] NOT IN (NULL, "None")
THEN Request.Data["TX_UOM"].ExtractDecimals().First()_" ##K-Cups per "_Request.Data["TX_UOM"].Split("/").Last()
ELSE "@@";

--<Nutritional Information (if available)>

IF A[11359].Values.Where("%serving") IS NOT NULL AND A[11248].Values IS NOT NULL
THEN "Each ##K-Cup includes "_A[11248].Match(11248, 11249).Values.UseSeparators(" (").Postfix(")").Erase(" (<>)").WhereNot("%(0 %").FlattenWithAnd().Replace("(1 g)", "(1 gram)").Replace(
" g)", " grams)").Replace(" mg)", "mg)").Replace(" ", " ##").Prefix("##")

ELSE "@@";

--<Free Trade information (if available)>
--https://www.staples.com/product_GMT6783CT        Fair Trade Certified™ flavored [type] ##K-Cups for ##Keurig brewers. 

IF A[6585].Value LIKE "Yes" 
THEN "Fair ##Trade ##Certified™ flavored "_$SP-17882$_" ##K-Cups for ##Keurig brewers" 
ELSE "@@";

--<Use for additional product and/or manufacturer information relevant to the customer buying decision>
IF $SP-17884$ LIKE "Yes" 
THEN "The product is kosher";

--<Use for additional product and/or manufacturer information relevant to the customer buying decision>
IF A[6739].Where("USDA Organic").Values IS NOT NULL
THEN "Meets or exceeds USDA ##Organic standard";

--<Use for additional product and/or manufacturer information relevant to the customer buying decision>
--Only Arabica
--https://www.staples.com/product_2710071        Made with 100% Arabica coffee 

IF $SP-350824$ LIKE "Arabica" 
THEN "Made with 100% arabica coffee" 

ELSE IF $SP-350824$ LIKE "Espresso" 
THEN "Offers the rich aromatics and flavor qualities of espresso, tailored specifically for the unique brewing parameters of a K-Cup portion pack";

--<Use for additional product and/or manufacturer information relevant to the customer buying decision>
IF A[6739].Values.Where("gluten-free") IS NOT NULL
THEN "Gluten-free";

--<Use for additional product and/or manufacturer information relevant to the customer buying decision>
--It contains no sugar, so it won't break your low-carb diet    https://www.staples.com/5-Hour-Energy-Berry-Drinks-1-93-oz-Bottles-12-Pack/product_897444
IF A[6739].Values.Where("sugar-free") IS NOT NULL
THEN "It contains no sugar, so it won't break your low-carb diet";

IF SKU.ProductId IN ("20658244", "20658237")
THEN "Blend of decaffeinated green tea and decaffeinated ##Bai ##Mu ##Dan white tea" 
ELSE "@@";