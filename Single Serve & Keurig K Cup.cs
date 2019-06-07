//§§1684103810253164886 "Single Serve & Keurig® K-Cup® Pods" "Alex K."

TypeAndCaffeine();
RoastAndFlavorKCup();
// Pack size
NutritionalInformation();
FreeTradeInformation();
// Kosher
AdditionalUSDAOrganic();
AdditionalArabica();

// Additional Gluten Free
// Additional SugarFree
AdditionalMixedTea();
AdditionalEspresso();

// --[FEATURE #1]
// --Type and Caffeine
void TypeAndCaffeine() {
    // Hot Chocolate|Seasonal|Tea|Dispensers|Accessories|Coffee
    var type = REQ.GetVariable("SP-17882").HasValue() ? REQ.GetVariable("SP-17882") : R("SP-17882").HasValue() ? R("SP-17882") : R("cnet_common_SP-17882");
    var caff = REQ.GetVariable("SP-17863").HasValue() ? REQ.GetVariable("SP-17863") : R("SP-17863").HasValue() ? R("SP-17863") : R("cnet_common_SP-17863");
    if (type.HasValue() && caff.HasValue()) {
        if (type.HasValue("Tea") && A[6721].HasValue()) {
            Add($"TypeAndCaffeine⸮{caff} {A[6721].Values.Select(o => o.Value()).FlattenWithAnd()} tea ##K-Cups for ##Keurig brewers");
        } else {
            Add($"TypeAndCaffeine⸮{caff} {type.ToLower(true)} ##K-Cups for ##Keurig brewers");
        }
    }
    else if (type.HasValue()) {
        Add($"TypeAndCaffeine⸮{type.ToLower(true).ToUpperFirstChar()} ##K-Cups for ##Keurig brewers");
    }
}
// --[FEATURE #2]
// --Roast and Flavor


void RoastAndFlavorKCup() {
    //Cookie|Arabica|Black Silk|Seasonal|Morning Blend|Sumatra Reserve|Our Blend|Cinnamon|Mudslide|Butter Toffee|Colombia Select|Breakfast Blend|Caramel Vanilla|Pecan|Classic Roast|French Roast|Veranda Blend|Mocha Nut Fudge|French Vanilla|Hazelnut|Colombian|Other|Chai Latt�|Unflavored|Pumpkin Spice|Full|Blueberry|Chocolate|Fruit Brew|Caramel|Mocha|Coconut|Cinnamon|Sweet|Gingerbread|Cappuccino|Dark Roast|Hawaiian Blend|Coconut Mocha|Organic Blend|Original Blend|Italian Roast|Vanilla|Jamaica Me Crazy|Guatemalan|Ultra Roast|Mild|Costa Rican|Caf� Almond Biscotti|Bold|Variety Pack|Green|Black|Sumatra|Herbal|Peach|Extra Bold|Espresso|Kahlua|Original Roast|Nantucket Blend|Lemon|Donut Blend|Pike Place|Creme Brulee|Caff� Verona|Caribou Blend|Buffalo Soldier|Dark Italian Roast|Country Blend|Special Blend|House Blend|Variety Pack|Dark Caffe Verona Blend|Signature Blend
    var flavoring = REQ.GetVariable("SP-17872").HasValue() ? REQ.GetVariable("SP-17872") : R("SP-17872").HasValue() ? R("SP-17872") : R("cnet_common_SP-17872"); // Cnet_flavoring
    //Medium|Variety Pack|Espresso|Medium Dark|Light|Blonde|Extra Dark|Dark
    var coffeeRoast = REQ.GetVariable("SP-17870").HasValue() ? REQ.GetVariable("SP-17870") : R("SP-17870").HasValue() ? R("SP-17870") : R("cnet_common_SP-17870");
    // Hot Chocolate|Seasonal|Tea|Dispensers|Accessories|Coffee
    var type = REQ.GetVariable("SP-17882").HasValue() ? REQ.GetVariable("SP-17882") : R("SP-17882").HasValue() ? R("SP-17882") : R("cnet_common_SP-17882");
    
    if (!coffeeRoast.HasValue("Variety pack") && coffeeRoast.HasValue() 
    && flavoring.HasValue("French roast") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮French roast coffee with ##French-roast flavor");
    }
    else if (!coffeeRoast.HasValue("Variety pack") && coffeeRoast.HasValue() 
    && !flavoring.HasValue("Variety Pack") &&  flavoring.HasValue() 
    && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToUpperFirstChar()} roast coffee with ##{flavoring}");
    }
    else if (type.HasValue("Seasonal") && A[6726].Where("brown sugar","%apple%").Count() == 2) {
        Add($"RoastAndFlavorKCup⸮Fresh, juicy goodness of sweet orchard apples with a hint of brown sugar");
    }
    else if (coffeeRoast.HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast} roasted coffee with ##{A[6725].Where("%vanila%", "%caramel%").First()} flavor to satisfy sweet cravings");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue() && A[6725].HasValue("%vanila%", "%caramel%") 
    && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {Coalesce(coffeeRoast, A[6731].FirstValue().ToUpperFirstChar())} roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd()} to satisfy sweet cravings");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue() && flavoring.In("Vanilla", "caramel") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {Coalesce(coffeeRoast, A[6731].FirstValue().ToUpperFirstChar())} roast coffee with ##{flavoring} to satisfy sweet cravings");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue() && A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{Coalesce(coffeeRoast, A[6731].FirstValue().ToUpperFirstChar())} roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd()} flavor" );
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue() && !flavoring.HasValue("Variety Pack") 
    && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValue().ToUpperFirstChar()}-roast coffee with ##{coffeeRoast}");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue() && !flavoring.HasValue("Variety Pack") 
    && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValue().ToUpperFirstChar()}-roast coffee with ##{coffeeRoast}");
    }
    else if (A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A great tasting Coffee with " 
         + $"{A[6725].Values.Select(o => "##" + o.Value().ToUpperFirstChar()).FlattenWithAnd()} flavoring");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A great tasting Coffee with ##{flavoring} flavoring");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("dark") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A great tasting dark roast coffee");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("medium") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮The medium roast gives you a strong coffee taste that isn't overpowering");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("medium dark") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A hearty, full-bodied blend of medium and dark roasts");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("light") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A lighter roasted coffee is buttery and sweet");
    }
    else if (!coffeeRoast.HasValue("Variety Pack") && coffeeRoast.HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast} roast coffee");
    }
    else if (!coffeeRoast.HasValue("Variety Pack") && coffeeRoast.HasValue() && type.HasValue("tea")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast} tea");
    }
    else if (A[6721].HasValue() && type.HasValue("tea")) {
        Add($"RoastAndFlavorKCup⸮{A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} tea");    
        
    }
    else if (!flavoring.In("Variety Pack", "%Chocolate%") && flavoring.HasValue() && type.HasValue("%Chocolate")) {
        Add($"RoastAndFlavorKCup⸮Hot Chocolate with ##{flavoring} flavor");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue() && type.HasValue("Seasonal")) {
        Add($"RoastAndFlavorKCup⸮Seasonal ##K-Cups with ##{flavoring} flavor");
    }
}

// --[FEATURE #3]
// --Container Size

// --[FEATURE #4]
// --Nutritional Information (if available)
void NutritionalInformation() {
    if (A[11359].HasValue("%serving") && A[11248].HasValue() && A[11249].HasValue()) {
        if (A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten("|").HasValue()
        && A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x <NULL>", "%x 0")).Flatten("|").HasValue()) {
            var units = A[11249].GetValuesWithUnits().Select(o => o.Value.Value() + " x " + o.Unit.Name).Where(s => !s.Contains("0 x")).Flatten("|").Replace(" x", ""," g", " grams", " mg", "mg", "1 grams", "1 gram").ToString().Split("|");
            var ingradiets = A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x 0", "%x <NULL>")).Flatten("|").RegexReplace("( x [0-9]+)", "").ToString().Split("|");
            if (units.Length > 0 && (units.Length == ingradiets.Length)) {
                var index = 0;
                var endIndex = units.Length;
                var arr = new List<string>();
                foreach (var item in units) {
                    if (index + 1 == endIndex) {
                        arr.Add($"and {ingradiets[index]} ({units[index]})");
                    }
                    else {
                        arr.Add($"{ingradiets[index]} ({units[index]})");
                    }
                    index++;
                }
                Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten(", ").Replace(" x <NULL>", "").RegexReplace(" ([A-Z])", " ##$1")}" 
                    + $", {Coalesce(String.Join(", ", arr)).RegexReplace(" ([A-Z])", " ##$1")}" );
            }
            else {
                 Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten(", ").Replace(" x <NULL>", "").RegexReplace(" ([A-Z])", " ##$1")}");
            }
        }
        else if (A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten("|").HasValue()) {
             Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).FlattenWithAnd().Replace(" x <NULL>", "").RegexReplace(" ([A-Z])", " ##$1")}");
        }
        else if (A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x <NULL>", "%x 0")).Flatten("|").HasValue()) {
            var units = A[11249].GetValuesWithUnits().Select(o => o.Value.Value() + " x " + o.Unit.Name).Where(s => !s.Contains("0 x")).Flatten("|").Replace(" x", ""," g", " grams", " mg", "mg", "1 grams", "1 gram").ToString().Split("|");
            var ingradiets = A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x 0", "%x <NULL>")).Flatten("|").RegexReplace("( x [0-9]+)", "").ToString().Split("|");
            if (units.Length > 0 && (units.Length == ingradiets.Length)) {
                var index = 0;
                var endIndex = units.Length;
                var arr = new List<string>();
                foreach (var item in units) {
                    if (endIndex == 1) {
                        
                    }
                    if (index + 1 == endIndex) {
                        arr.Add($"and {ingradiets[index]} ({units[index]})");
                    }
                    else {
                        arr.Add($"{ingradiets[index]} ({units[index]})");
                    }
                    index++;
                }
                Add($"NutritionalInformation⸮Each ##K-Cup includes {Coalesce(String.Join(", ", arr)).RegexReplace(" ([A-Z])", " ##$1")}" );
            }
        }
    }
}

// --[FEATURE #5]
// --Free Trade information (if available)
void FreeTradeInformation() {
    if (A[6585].HasValue("Yes")) {
        Add($"FreeTradeInformation⸮Fair ##Trade ##Certified");
    }
}

// --[FEATURE #6]
// --Additional kosher

// --[FEATURE #7]
// --Additional USDA Organic

void AdditionalUSDAOrganic() {
    if (A[6739].HasValue("USDA Organic")) {
        Add($"AdditionalUSDAOrganic⸮Meets or exceeds USDA ##Organic standard");
    }
}
// --[FEATURE #8]
// --Additional Arabica
void AdditionalArabica() {
    var type = REQ.GetVariable("SP-350824").HasValue() ? REQ.GetVariable("SP-350824") : R("SP-350824").HasValue() ? R("SP-350824") : R("cnet_common_SP-350824");
    if (type.HasValue("Arabica")) {
        Add($"AdditionalArabica⸮Made with 100% arabica coffee");
    }
}

// --[FEATURE #9]
// --Additional Espresso
void AdditionalEspresso() {
    var type = REQ.GetVariable("SP-350824").HasValue() ? REQ.GetVariable("SP-350824") : R("SP-350824").HasValue() ? R("SP-350824") : R("cnet_common_SP-350824");
    if (type.HasValue("Espresso")) {
        Add($"AdditionalEspresso⸮Offers the rich aromatics and flavor qualities of espresso, tailored specifically for the unique brewing parameters of a K-Cup portion pack");
    }
}

// --[FEATURE #10]
// --Additional Gluten Free


// --[FEATURE #11]
// --Additional SugarFree

// --[FEATURE #12]
// -- void AdditionalMixedTea() {
    if (SKU.ProductId.In("20658244", "20658237"))
    {
        Add("AdditionalMixedTea⸮Blend of decaffeinated green tea and decaffeinated ##Bai ##Mu ##Dan white tea");
    }
}

//§§1684103810253164886 end of "Single Serve & Keurig® K-Cup® Pods"