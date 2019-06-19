// --[FEATURE #2]
// --Roast and Flavor
void RoastAndFlavorKCup() {
    //Cookie|Arabica|Black Silk|Seasonal|Morning Blend|Sumatra Reserve|Our Blend|Cinnamon|Mudslide|Butter Toffee|Colombia Select|Breakfast Blend|Caramel Vanilla|Pecan|Classic Roast|French Roast|Veranda Blend|Mocha Nut Fudge|French Vanilla|Hazelnut|Colombian|Other|Chai Latt�|Unflavored|Pumpkin Spice|Full|Blueberry|Chocolate|Fruit Brew|Caramel|Mocha|Coconut|Cinnamon|Sweet|Gingerbread|Cappuccino|Dark Roast|Hawaiian Blend|Coconut Mocha|Organic Blend|Original Blend|Italian Roast|Vanilla|Jamaica Me Crazy|Guatemalan|Ultra Roast|Mild|Costa Rican|Caf� Almond Biscotti|Bold|Variety Pack|Green|Black|Sumatra|Herbal|Peach|Extra Bold|Espresso|Kahlua|Original Roast|Nantucket Blend|Lemon|Donut Blend|Pike Place|Creme Brulee|Caff� Verona|Caribou Blend|Buffalo Soldier|Dark Italian Roast|Country Blend|Special Blend|House Blend|Variety Pack|Dark Caffe Verona Blend|Signature Blend
    var flavoring = REQ.GetVariable("SP-15240").HasValue() ? REQ.GetVariable("SP-15240") : R("SP-15240").HasValue() ? R("SP-15240") : R("cnet_common_SP-15240"); // Cnet_flavoring
    //Medium|Variety Pack|Espresso|Medium Dark|Light|Blonde|Extra Dark|Dark
    var coffeeRoast = REQ.GetVariable("SP-17870").HasValue() ? REQ.GetVariable("SP-17870") : R("SP-17870").HasValue() ? R("SP-17870") : R("cnet_common_SP-17870");
    // Hot Chocolate|Seasonal|Tea|Dispensers|Accessories|Coffee
    var type = REQ.GetVariable("SP-17882").HasValue() ? REQ.GetVariable("SP-17882") : R("SP-17882").HasValue() ? R("SP-17882") : R("cnet_common_SP-17882");
    var hotIced = REQ.GetVariable("SP-17881").HasValue() ? REQ.GetVariable("SP-17881") : R("SP-17881").HasValue() ? R("SP-17881") : R("cnet_common_SP-17881");
    
    
    if (coffeeRoast.HasValue("Variety Pack")) {
         Add($"RoastAndFlavorKCup⸮Variety of flavoring to discover the delicious tastes of Keurig");
    }
    else if (!coffeeRoast.HasValue("Variety pack") && coffeeRoast.HasValue() 
    && flavoring.HasValue("French roast") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮French roast coffee with ##French-roast flavor");
    }
    else if (!coffeeRoast.HasValue("Variety pack") && coffeeRoast.HasValue() 
    && !flavoring.HasValue("Variety Pack") &&  flavoring.HasValue() 
    && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with ##{flavoring} flavor");
    }
    else if (type.HasValue("Seasonal") && A[6726].Where("brown sugar","%apple%").Count() == 2) {
        Add($"RoastAndFlavorKCup⸮Fresh, juicy goodness of sweet orchard apples with a hint of brown sugar");
    }
    else if (coffeeRoast.HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast} roasted coffee with ##{A[6725].Where("%vanila%", "%caramel%").First()} flavor to satisfy sweet cravings");
    }
    // ---------
    else if (coffeeRoast.HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast.ToLower(true)}-roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd()} to satisfy sweet cravings");
    }
    else if (A[6731].HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {A[6731].FirstValueOrDefault().ToLower(true)} roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd()} to satisfy sweet cravings");
    }
    // <<<
    else if (coffeeRoast.HasValue() && flavoring.In("Vanilla", "caramel") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast.ToLower(true)}-roast coffee with ##{flavoring} to satisfy sweet cravings");
    }
    else if (A[6731].HasValue() && flavoring.In("Vanilla", "caramel") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {A[6731].FirstValueOrDefault().ToLower(true)}-roast coffee with ##{flavoring} to satisfy sweet cravings");
    }
    // <<<<
      else if (coffeeRoast.HasValue() && A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd()} flavor" );
    }
      else if (A[6731].HasValue() && A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValueOrDefault().ToUpperFirstChar()}-roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd()} flavor" );
    }
    // <<<<
    
    else if (coffeeRoast.HasValue() && !flavoring.HasValue("Variety Pack") 
    && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with ##{coffeeRoast} flavor");
    }
    
     else if (A[6731].HasValue() && !flavoring.HasValue("Variety Pack") && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValueOrDefault().ToUpperFirstChar()}-roast coffee with ##{coffeeRoast} flavor");
    }
    // <<<
    else if (coffeeRoast.HasValue() && !flavoring.HasValue("Variety Pack") && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with ##{coffeeRoast} flavor");
    }
    else if (A[6731].HasValue() && !flavoring.HasValue("Variety Pack") && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValueOrDefault().ToUpperFirstChar()}-roast coffee with ##{coffeeRoast} flavor");
    }
    // ------------
    else if (A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮Great tasting coffee with " 
         + $"{A[6725].Values.Select(o => "##" + o.Value().ToUpperFirstChar()).FlattenWithAnd()} flavoring");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮Great tasting coffee with ##{flavoring} flavoring");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("dark") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮Great tasting dark roast coffee");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("medium") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮The medium roast gives you a strong coffee taste that isn't overpowering");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("medium dark") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A hearty, full-bodied blend of medium and dark roasts");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("light") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A light roasted coffee is buttery and sweet");
    }
    else if (!coffeeRoast.HasValue("Variety Pack") && coffeeRoast.HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()} roast coffee");
    }
    else if (!coffeeRoast.HasValue("Variety Pack") && coffeeRoast.HasValue() && type.HasValue("tea")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()} tea");
        
    }
     else if (A[6721].HasValue() && type.HasValue("tea") && hotIced.HasValue("Iced") && flavoring.HasValue()) {
        Add($"RoastAndFlavorKCup⸮{flavoring} iced {A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} tea tastes pleasant");    
    }
     else if (A[6721].HasValue() && type.HasValue("tea") && flavoring.HasValue()) {
        Add($"RoastAndFlavorKCup⸮{flavoring} {A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} tea tastes pleasant");    
        
    }
    else if (A[6721].HasValue() && type.HasValue("tea")) {
        Add($"RoastAndFlavorKCup⸮{A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} tea tastes pleasant");    
        
    }
    else if (!flavoring.In("Variety Pack", "%Chocolate%") && flavoring.HasValue() && type.HasValue("%Chocolate")) {
        Add($"RoastAndFlavorKCup⸮Hot сhocolate with ##{flavoring} flavor");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue() && type.HasValue("Seasonal")) {
        Add($"RoastAndFlavorKCup⸮Seasonal ##K-Cups with ##{flavoring} flavor");
    }
}






// --[FEATURE #2]
// --Roast and Flavor
void RoastAndFlavorKCup() {
    //Cookie|Arabica|Black Silk|Seasonal|Morning Blend|Sumatra Reserve|Our Blend|Cinnamon|Mudslide|Butter Toffee|Colombia Select|Breakfast Blend|Caramel Vanilla|Pecan|Classic Roast|French Roast|Veranda Blend|Mocha Nut Fudge|French Vanilla|Hazelnut|Colombian|Other|Chai Latt�|Unflavored|Pumpkin Spice|Full|Blueberry|Chocolate|Fruit Brew|Caramel|Mocha|Coconut|Cinnamon|Sweet|Gingerbread|Cappuccino|Dark Roast|Hawaiian Blend|Coconut Mocha|Organic Blend|Original Blend|Italian Roast|Vanilla|Jamaica Me Crazy|Guatemalan|Ultra Roast|Mild|Costa Rican|Caf� Almond Biscotti|Bold|Variety Pack|Green|Black|Sumatra|Herbal|Peach|Extra Bold|Espresso|Kahlua|Original Roast|Nantucket Blend|Lemon|Donut Blend|Pike Place|Creme Brulee|Caff� Verona|Caribou Blend|Buffalo Soldier|Dark Italian Roast|Country Blend|Special Blend|House Blend|Variety Pack|Dark Caffe Verona Blend|Signature Blend
    var flavoring = REQ.GetVariable("SP-15240").HasValue() ? REQ.GetVariable("SP-15240") : R("SP-15240").HasValue() ? R("SP-15240") : R("cnet_common_SP-15240"); // Cnet_flavoring
    //Medium|Variety Pack|Espresso|Medium Dark|Light|Blonde|Extra Dark|Dark
    var coffeeRoast = REQ.GetVariable("SP-17870").HasValue() ? REQ.GetVariable("SP-17870") : R("SP-17870").HasValue() ? R("SP-17870") : R("cnet_common_SP-17870");
    // Hot Chocolate|Seasonal|Tea|Dispensers|Accessories|Coffee
    var type = REQ.GetVariable("SP-17882").HasValue() ? REQ.GetVariable("SP-17882") : R("SP-17882").HasValue() ? R("SP-17882") : R("cnet_common_SP-17882");
    var hotIced = REQ.GetVariable("SP-17881").HasValue() ? REQ.GetVariable("SP-17881") : R("SP-17881").HasValue() ? R("SP-17881") : R("cnet_common_SP-17881");
    
    
    if (coffeeRoast.HasValue("Variety Pack")) {
         Add($"RoastAndFlavorKCup⸮Variety of flavoring to discover the delicious tastes of Keurig");
    }
    else if (!coffeeRoast.HasValue("Variety pack") && coffeeRoast.HasValue() 
    && flavoring.HasValue("French roast") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮French roast coffee with french-roast flavor");
    }
    else if (!coffeeRoast.HasValue("Variety pack") && coffeeRoast.HasValue() 
    && !flavoring.HasValue("Variety Pack") &&  flavoring.HasValue() 
    && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with {flavoring.ToLower(true)} flavor");
    }
    else if (type.HasValue("Seasonal") && A[6726].Where("brown sugar","%apple%").Count() == 2) {
        Add($"RoastAndFlavorKCup⸮Fresh, juicy goodness of sweet orchard apples with a hint of brown sugar");
    }
    else if (coffeeRoast.HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast.ToLower(true)} roasted coffee with ##{A[6725].Where("%vanila%", "%caramel%").First().Value().ToLower(true)} flavor to satisfy sweet cravings");
    }
    // ---------
    else if (coffeeRoast.HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast.ToLower(true)}-roast coffee with {A[6725].Values.Select(o => o.Value()).FlattenWithAnd().ToLower(true)} to satisfy sweet cravings");
    }
    else if (A[6731].HasValue() && A[6725].HasValue("%vanila%", "%caramel%") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {A[6731].FirstValueOrDefault().ToLower(true)} roast coffee with {A[6725].Values.Select(o => o.Value()).FlattenWithAnd().ToLower(true)} to satisfy sweet cravings");
    }
    // <<<
    else if (coffeeRoast.HasValue() && flavoring.In("Vanilla", "caramel") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {coffeeRoast.ToLower(true)}-roast coffee with {flavoring.ToLower(true)} to satisfy sweet cravings");
    }
    else if (A[6731].HasValue() && flavoring.In("Vanilla", "caramel") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A smooth, {A[6731].FirstValueOrDefault().ToLower(true)}-roast coffee with {flavoring.ToLower(true)} to satisfy sweet cravings");
    }
    // <<<<
      else if (coffeeRoast.HasValue() && A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd().ToLower(true)} flavor" );
    }
      else if (A[6731].HasValue() && A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValueOrDefault().ToLower().ToUpperFirstChar()}-roast coffee with {A[6725].Values.Select(o => "##" + o.Value()).FlattenWithAnd().ToLower(true)} flavor" );
    }
    // <<<<
    
    else if (coffeeRoast.HasValue() && !flavoring.HasValue("Variety Pack") 
    && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with ##{flavoring.ToLower()} flavor");
    }
    
     else if (A[6731].HasValue() && !flavoring.HasValue("Variety Pack") && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValueOrDefault().ToLower().ToUpperFirstChar()}-roast coffee with ##{flavoring.ToLower()} flavor");
    }
    // <<<
    else if (coffeeRoast.HasValue() && !flavoring.HasValue("Variety Pack") && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()}-roast coffee with ##{flavoring.ToLower()} flavor");
    }
    else if (A[6731].HasValue() && !flavoring.HasValue("Variety Pack") && flavoring.HasValue() ) {
        Add($"RoastAndFlavorKCup⸮{A[6731].FirstValueOrDefault().ToLower().ToUpperFirstChar()}-roast coffee with ##{flavoring.ToLower()} flavor");
    }
    // ------------
    else if (A[6725].HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮Great tasting coffee with " 
         + $"{A[6725].Values.Select(o => o.Value()).FlattenWithAnd().ToLower()} flavoring");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮Great tasting coffee with ##{flavoring.ToLower()} flavoring");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("dark") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮Great tasting dark roast coffee");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("medium") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮The medium roast gives you a strong coffee taste that isn't overpowering");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("medium dark") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A hearty, full-bodied blend of medium and dark roasts");
    }
    else if (Coalesce(coffeeRoast, A[6731]).HasValue("light") && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮A light roasted coffee is buttery and sweet");
    }
    else if (!coffeeRoast.HasValue("Variety Pack") && coffeeRoast.HasValue() && type.HasValue("Coffee")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()} roast coffee");
    }
    else if (!coffeeRoast.HasValue("Variety Pack") && coffeeRoast.HasValue() && type.HasValue("tea")) {
        Add($"RoastAndFlavorKCup⸮{coffeeRoast.ToLower(true).ToUpperFirstChar()} tea");
        
    }
    else if (A[6721].HasValue() && type.HasValue("tea") && hotIced.HasValue("Iced") && flavoring.HasValue()) {
        Add($"RoastAndFlavorKCup⸮{flavoring.ToLower().ToUpperFirstChar()} iced {A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToLower()} tea tastes pleasant");    
    }
     else if (A[6721].HasValue() && type.HasValue("tea") && flavoring.HasValue()) {
        Add($"RoastAndFlavorKCup⸮{flavoring.ToLower().ToUpperFirstChar()} {A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToLower()} tea tastes pleasant");    
        
    }
    else if (A[6721].HasValue() && type.HasValue("tea")) {
        Add($"RoastAndFlavorKCup⸮{A[6721].Values.Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} tea tastes pleasant");    
        
    }
    else if (!flavoring.In("Variety Pack", "%Chocolate%") && flavoring.HasValue() && type.HasValue("%Chocolate")) {
        Add($"RoastAndFlavorKCup⸮Hot сhocolate with ##{flavoring.ToLower()} flavor");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue() && type.HasValue("Seasonal")) {
        Add($"RoastAndFlavorKCup⸮Seasonal ##K-Cups with ##{flavoring.ToLower()} flavor");
    }
    else if (!flavoring.HasValue("Variety Pack") && flavoring.HasValue()) {
        Add($"RoastAndFlavorKCup⸮##K-Cups with ##{flavoring.ToLower()} flavor");
    }
}



// --[FEATURE #3]
// --Capacity (oz.) (Should be Capacity of individual unit in oz. if applicable. If not applicable it should be the total Capacity in oz.)
void CoffeCapacity() {
    var capacity = REQ.GetVariable("SP-21132").HasValue() ? REQ.GetVariable("SP-21132") : R("SP-21132").HasValue() ? R("SP-21132") : R("cnet_common_SP-21132");
    if (capacity.HasValue() && A[6718].HasValue("%bags")) {
        Add($"CoffeCapacity⸮{capacity}oz. individual portion packs"); 
    }
    else if (A[6728].HasValue() && A[6728].Units.First().NameUSM.HasValue("%oz%")) {
        Add($"CoffeCapacity⸮Brews an {A[6728].FirstValueUsm()}oz. cup of coffee");
    }
    else if (A[6728].HasValue() && A[6728].Units.First().NameUSM.HasValue("%lb%")) {
        Add($"CoffeCapacity⸮Brews an {Math.Round((double)A[6728].FirstValueUsm().ExtractNumbers().First() * 16, 2)}oz. cup of coffee");
    }
    else if (capacity.HasValue()) {
        Add($"CoffeCapacity⸮Capacity: {capacity}oz.");
    }
}