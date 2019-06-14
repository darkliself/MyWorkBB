//§§1684103810253141073 "Coffee" "Alex K."
CoffeeTypeAndUse();
RoastAndFlavor();
CoffeCapacity();
CaffeinatedOrDecaf();
// Pack size
// Fair Trade Certified
// USDA Certification
// Additional Arabica
// Gluten-free
// SugarFree
// Additional vacuum packed


// --[FEATURE #1]
// --Coffee type & use


void CoffeeTypeAndUse() {
    // K-Cup|Pods|Tassimo Discs|Bolt Pack|Cold Brew|Ground|Vue Pack|Beans|Instant|Liquid Concentrate|Liquid|Capsules|Filter Packs|Cartridges|Freshpack
    
    var coffeeType = REQ.GetVariable("SP-17871").HasValue() ? REQ.GetVariable("SP-17871") : R("SP-17871").HasValue() ? R("SP-17871") : R("cnet_common_SP-17871");
;
    if (coffeeType.HasValue("ground") || A[6718].HasValue("ground%")) {
        Add($"TypeAndCaffeine⸮Ground coffee is ready for brewing");
    }
    else if (coffeeType.HasValue("Beans") || A[6718].HasValue("%bean%")) {
        Add($"TypeAndCaffeine⸮Whole coffee beans are ideal for coffee lovers who like the ultimate control over their brew");
    }
    else if (coffeeType.HasValue("Pods") || A[6718].HasValue("coffee%pod%")) {
        Add($"TypeAndCaffeine⸮Whole coffee beans are ideal for coffee lovers who like the ultimate control over their brew");
    }
    else if (coffeeType.HasValue() || A[10018].HasValue("T-Disc")) {
        Add($"TypeAndCaffeine⸮Brewed to perfection with ##Tassimo ##Intelligent ##Barcode technology");
    }
    else if (coffeeType.HasValue("Cold Brew")) {
         Add($"TypeAndCaffeine⸮Cold brew coffee delivers natural coffee flavor in a smooth-tasting beverage");
    }
    else if (coffeeType.HasValue("Cartridges")) {
        Add($"TypeAndCaffeine⸮To use, place a cartridge in the machine, close the door, and press button to brew");
    }
    else if (coffeeType.HasValue("Capsules")) {
        Add($"TypeAndCaffeine⸮Simply pop in the coffee capsule and brew");
    }
}

// --[FEATURE #2]
// --Coffee roast & Flavor (As stated by the Manufacturer)

void RoastAndFlavor() {
    // K-Cup|Pods|Tassimo Discs|Bolt Pack|Cold Brew|Ground|Vue Pack|Beans|Instant|Liquid Concentrate|Liquid|Capsules|Filter Packs|Cartridges|Freshpack
    var flavor = REQ.GetVariable("SP-17872").HasValue() ? REQ.GetVariable("SP-17872") : R("SP-17872").HasValue() ? R("SP-17872") : R("cnet_common_SP-17872");
    var roast = REQ.GetVariable("SP-17870").HasValue() ? REQ.GetVariable("SP-17870") : R("SP-17870").HasValue() ? R("SP-17870") : R("cnet_common_SP-17870");
;
    if (flavor.HasValue() && roast.HasValue() && !flavor.HasValue("Variety Pack")) {
        Add($"RoastAndFlavor⸮{roast.ToTitleCase().Replace(" ", " ##")}-roast coffee with {flavor.Replace("colombian", "##Colombian", "Original Blend", "##Original ##Blend")} flavor"); 
    }

    else if (roast.HasValue() && A[6725].HasValue("%vanila%", "%caramel%")) {
        Add($"RoastAndFlavor⸮A smooth, {roast} roasted coffee with ##{A[6725].Where("%vanila%", "%caramel%").First()} flavor to satisfy sweet cravings");
    }
    else if (A[6731].HasValue() && A[6725].HasValue("%vanila%", "%caramel%")) {
        Add($"RoastAndFlavor⸮A smooth, {A[6731].FirstValueOrDefault()}-roasted coffee with ##{A[6725].Where("%vanila%", "%caramel%").First()} flavor to satisfy sweet cravings");
    }

    else if (roast.HasValue() && flavor.In("Vanilla", "Caramel")) {
        Add($"RoastAndFlavor⸮A smooth, {roast} roasted coffee with ##{flavor} flavor to satisfy sweet cravings");
    }
    else if (A[6731].HasValue() && flavor.In("Vanilla", "Caramel")) {
        Add($"RoastAndFlavor⸮A smooth, {A[6731].FirstValueOrDefault()}-roasted coffee with ##{flavor} flavor to satisfy sweet cravings");
    }

    else if (roast.HasValue() && A[6725].HasValue()) {
        Add($"RoastAndFlavor⸮{roast.ToUpperFirstChar()} roasted coffee with ##{A[6725].Values.Select(o => "##" + o.Value().ToUpperFirstChar()).FlattenWithAnd()} flavor");
    }
    else if (A[6731].HasValue() && A[6725].HasValue()) {
        Add($"RoastAndFlavor⸮{A[6731].FirstValueOrDefault().ToUpperFirstChar()} roasted coffee with ##{A[6725].Values.Select(o => "##" + o.Value().ToUpperFirstChar()).FlattenWithAnd()} flavor");
    }

     else if (roast.HasValue() && flavor.HasValue()) {
        Add($"RoastAndFlavor⸮{roast.ToUpperFirstChar()} roasted coffee with ##{flavor} flavor");
    }
    else if (A[6731].HasValue() && flavor.HasValue()) {
        Add($"RoastAndFlavor⸮{A[6731].FirstValueOrDefault().ToUpperFirstChar()} roasted coffee with ##{flavor} flavor");
    }

    else if (A[6725].HasValue()) {
        Add($"RoastAndFlavor⸮A great tasting coffee with {A[6725].Values.Select(o => "##" + o.Value().ToUpperFirstChar()).FlattenWithAnd()} flavoring");
    }

    else if (flavor.HasValue() && !flavor.HasValue("Variety Pack")) {
       Add($"RoastAndFlavor⸮A great tasting coffee with ##{flavor.ToTitleCase().Replace(" ", " ##", "Roast", "roast")} flavoring");
    }

    else if (roast.HasValue("dark") || A[6731].HasValue("dark")) {
       Add($"RoastAndFlavor⸮A great tasting dark roast coffee");
    }

    else if (roast.HasValue("medium") || A[6731].HasValue("medium")) {
       Add($"RoastAndFlavor⸮The medium roast gives you a strong coffee taste that isn't overpowering");
    }

    else if (roast.HasValue("Medium Dark") || A[6731].HasValue("Medium Dark")) {
       Add($"RoastAndFlavor⸮A hearty, full-bodied blend of medium and dark roasts");
    }
}

// --[FEATURE #3]
// --Capacity (oz.) (Should be Capacity of individual unit in oz. if applicable. If not applicable it should be the total Capacity in oz.)
void CoffeCapacity() {
    var capacity = REQ.GetVariable("SP-21132").HasValue() ? REQ.GetVariable("SP-21132") : R("SP-21132").HasValue() ? R("SP-21132") : R("cnet_common_SP-21132");
    if (capacity.HasValue()) {
        Add($"CoffeCapacity⸮{capacity} oz. individual portion packs"); 
    }
    else if (A[6728].HasValue() && A[6728].Units.First().NameUSM.HasValue("%oz%")) {
        Add($"CoffeCapacity⸮Brews an {A[6728].FirstValueUsm()} oz. cup of coffee");
    }
    else if (A[6728].HasValue() && A[6728].Units.First().NameUSM.HasValue("%lb%")) {
        Add($"CoffeCapacity⸮Brews an {Math.Round((double)A[6728].FirstValueUsm().ExtractNumbers().First() * 16, 2)} oz. cup of coffee");
    }
}

// --[FEATURE #4]
// --Caffeinated or Decaf
void CaffeinatedOrDecaf() {
    var caffeinated = REQ.GetVariable("SP-17863").HasValue() ? REQ.GetVariable("SP-17863") : R("SP-17863").HasValue() ? R("SP-17863") : R("cnet_common_SP-17863");
    if (caffeinated.HasValue("Caffeinated")) {
        Add($"CaffeinatedOrDecaf⸮The brew is fully caffeinated"); 
    }
    else if (caffeinated.HasValue("Decaffeinated")) {
        Add($"CaffeinatedOrDecaf⸮Decaffeinated coffee provides a delectable way to enjoy a delicious cup of coffee without jolt");
    }
}

// --[FEATURE #5]
// --Pack Size (If more than 1)

// --[FEATURE #6]
// --Fair Trade Certified

// --[FEATURE #7]
// --USDA Certification

// --[FEATURE #8]
// --Additional Arabica

// --[FEATURE #9]
// --Additional gluten-free

// --[FEATURE #10]
// --Additional

// --[FEATURE #11]
// --Additional

// --[FEATURE #8]
// --Additional vacuum packed
void CoffeVacuumPacked() {
    if (A[6739].HasValue("vacuum packed")) {
        Add($"CoffeVacuumPacked⸮Vacuum sealed packets for consistently fresh taste, cup after cup"); 
    }
}

//§§1684103810253141073 end of "Coffee"

Cold Brew Coffee
delivers natural coffee flavor in a smooth-tasting beverage

The cold brew process delivers a beverage that is smoother, naturally sweeter than hot brewed coffee, and less acidic.