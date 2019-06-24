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


// "Bullet 2" "Post-it Size & Post-it color Collection" 
void PostItSizeColorCollection()
{
    var postItSizeColorCollection = "";
    var size = !(R("SP-18403") is null) || !R("SP-18403").Text.Equals("<NULL>") ? R("SP-18403").Text :
    !(R("cnet_common_SP-18403") is null) || !R("cnet_common_SP-18403").Text.Equals("<NULL>") ? R("cnet_common_SP-18403").Text : "";
    var width = !(R("SP-21044") is null) || !R("SP-21044").Text.Equals("<NULL>") ? R("SP-21044").Text :
    !(R("cnet_common_SP-21044") is null) || !R("cnet_common_SP-21044").Text.Equals("<NULL>") ? R("cnet_common_SP-21044").Text : "";// width in inches 
    var length = !(R("SP-20400") is null) || !R("SP-20400").Text.Equals("<NULL>") ? R("SP-20400").Text :
    !(R("cnet_common_SP-20400") is null) || !R("cnet_common_SP-20400").Text.Equals("<NULL>") ? R("cnet_common_SP-20400").Text : ""; // length in inches 
    var hasWidthAndLength = (!String.IsNullOrEmpty(width) && !String.IsNullOrEmpty(length));
    var colorCollection = R("SP-350290").Text;
    var paperColor = A[6022];
    if (!String.IsNullOrEmpty(size) && size.ToLower().Equals("other")
    && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L sticky notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && paperColor.HasValue("canary yellow")
    && colorCollection.Equals("Assorted")
    && (String.IsNullOrEmpty(size) || !size.ToLower().Equals("other"))
    && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L, Canary Yellow + assorted color notes";
    }
    else if (paperColor.HasValue("%canary yellow%") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the canary yellow attach without staples, paperclips, or tape";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("bora bora")
    && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Bora Bora Collection hold stronger and longer than other notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("bali") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Bali Collection hold stronger and longer than other notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("helsinki") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L Helsinki Collection notes are just the right size to carry around or keep on your desk";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("jaipur") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L Jaipur Collection notes stand out from the crowd to give you peace of mind";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("miami") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Miami Collection are a simple tool to keep your everyday organized";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("rio de janeiro") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"Eye-catching {width}\"W x {length}\"L Rio de Janeiro notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("rio de janeiro"))
    {
        postItSizeColorCollection = $"Grab even the busiest person's attention with the Rio de Janeiro Collection";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marrakesh") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marrakesh Collection keep your messages visible";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L New York notes include colors inspired by the skyline, stone and steel";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york"))
    {
        postItSizeColorCollection = $"New York Collection includes colors inspired by the skyline, stone and steel";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marseille") && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marseille Collection are a simple tool to keep you organized";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && hasWidthAndLength)
    {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the {colorCollection.Replace(@"Assorted", @"ass_orted")} Collection are a simple tool to keep you organized";
    }
    else if (colorCollection.ToLower().Contains("assorted"))
    {
        postItSizeColorCollection = "Assorted colors notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection))
    {
        postItSizeColorCollection = $"{colorCollection} Collection notes are a simple tool to keep your everyday organized";
    }
    else if (!String.IsNullOrEmpty(size))
    {
        postItSizeColorCollection = $"{Coalesce(size).RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1W$2$3L")} sticky notes";
    }

    if (!String.IsNullOrEmpty(postItSizeColorCollection))
    {
        Add($"PostItSizeColorCollection⸮{postItSizeColorCollection}");
    }
}



// "Bullet 3" "Sheet Count/Pads per Pack" 
void SheetCountPadsPack() {
    var result = "";
    var padsPerPack = GetReferenceBase("SP-350416");
    var pads_x_sheets = Coalesce(A[6110]);
    var pagesSheetsQty = A[6021];
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM"));
    var size = TX_UOM.HasValue("Dozen") ? 12 : TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0;
    var pack = TX_UOM.HasValue() ? TX_UOM.ToString().Split("/").Last() : "";
    var sheets = 0;
    var colorCollection = GetReferenceBase("SP-350290");
    if (pads_x_sheets.HasValue()) {
        sheets = pads_x_sheets.FirstValueOrDefault().ToString().Split("x").Count() == 2 ? pads_x_sheets.FirstValueOrDefault().ExtractNumbers().Last() : 0;
    }
    if (padsPerPack.HasValue() && !padsPerPack.HasValue("1")) {
         if (padsPerPack.HasValue("12")) {
            if (colorCollection.HasValue("assorted")) {
                result = "12 per pack";
            }
            else {
                result = "Dozen per pack";
            }
        }
        result = $"{padsPerPack} per pack";
    }
    if (pads_x_sheets.HasValue() && pads_x_sheets.FirstValueOrDefault().ToString().Split("x").Count() == 2
    && size != 0 && sheets != 0) {
        result = $"{sheets} notes per pad; {size} pads per pack";
    }
    else if (pagesSheetsQty.HasValue()) {
        result = $"They come in a pack of {pagesSheetsQty.FirstValueOrDefault()}";
    }
    else if (size != 0) {
        if (TX_UOM.HasValue("Dozen")) {
            if (colorCollection.HasValue("assorted")) {
                result = "12 per pack";
            }
            else {
                result = "Dozen per pack";
            }
        }
        result = $"{size} per {pack.ToLower()}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"SheetCountPadsPack⸮{result}");
    }
}