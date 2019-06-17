--[FEATURE #1]
--Blank media type & Use
--DVD-R provides ample storage space for your data
--DVD+R provides ample storage space for your data
IF $SP-18301$ IN ("DVD-R", "DVD+R")
THEN $SP-18301$_" provides ample storage space for your data" 

--Staples DVD+RW discs are ideal for data-intensive, high-performance and re-writable data storage.
ELSE IF $SP-18301$ IN ("DVD-RW", "DVD+RW")
THEN $SP-18301$_" discs are ideal for data-intensive, high-performance, and rewritable data storage" 

--Blu-ray Discs are an ideal personal or professional solution for storing large amounts of data 
ELSE IF $SP-18301$ LIKE "Blu-Ray" 
THEN "Blu-ray ##Discs are an ideal personal or professional solution for storing large amounts of data" 
--CD-R helps you safely burn your personal and work files for convenient use, anytime
ELSE IF $SP-18301$ LIKE "CD-R" 
THEN "CD-R helps you safely burn your personal and work files for convenient use anytime" 
--CD-RW can be used multiple times to store and share data
ELSE IF $SP-18301$ LIKE "CD-RW" 
THEN "CD-RW can be used multiple times to store and share data" 
--Data cartridge delivers faster access time and enhanced media monitoring
ELSE IF $SP-18301$ LIKE "Data Cartridges" 
THEN "Data cartridge delivers faster access time and enhanced media monitoring" 
--With functionality similar to an external hard drive, DVD-RAM is designed for data intensive, high-performance applications
ELSE IF $SP-18301$ LIKE "%DVD-RAM" 
THEN "DVD-RAM with functionality similar to an external drive for data-intensive applications" 
--NOT EXPECTED
ELSE IF $SP-18301$ IS NOT NULL
THEN $SP-18301$
ELSE "@@";

-- [FEATURE#2]
-- Max speed supported, Color & Printable

-- Verbatim DataLifePlus printable DVD-R discs make it easy to burn your files or videos for backup or distribution. Utilizing thermal printing technology, each of these discs offer a 16X write speed
IF $SP-21994$ IS NOT NULL
AND $SP-22967$ IS NOT NULL
AND $SP-21995$ IS NOT NULL
THEN $SP-22967$_" "_$SP-21995$_" discs offer "_$SP-21994$_"  write speed" 
ELSE IF $SP-21994$ IS NOT NULL
AND $SP-21995$ IS NOT NULL
THEN $SP-21995$_" discs offer "_$SP-21994$_"  write speed" 
-- Comes in white color
-- 52x write speed for quick data transfer 
ELSE IF $SP-21994$ IS NOT NULL
AND $SP-22967$ IS NOT NULL
THEN "Comes in "_$SP-22967$_" with "_$SP-21994$_ " write speed for quick data transfer" 
ELSE IF $SP-22967$ IS NOT NULL
AND $SP-21995$ IS NOT NULL
THEN $SP-21995$_" discs come in "_$SP-22967$
ELSE IF $SP-22967$ IS NOT NULL
THEN "Comes in "_$SP-22967$
-- Maximum write speed of 6x for high-speed performance
ELSE IF $SP-21994$ IS NOT NULL
THEN "Maximum write speed of "_$SP-21994$_" for high-speed performance" 
-- The Verbatim® DataLifePlus® 98319 DVD+R DL recordable media comes with white inkjet hub printable surface.
ELSE IF $SP-21995$ IS NOT NULL
THEN "Comes with "_$SP-21995$_" surface" 
ELSE "@@";

-- [FEATURE #3]
-- Blank media data capacity

-- These DVD-R 16X discs allow users to record up to 4.7GB of data
IF $cnet_capacity_blank_media$ IS NOT NULL
AND $SP-18301$ LIKE "Data Cartridges" 
THEN $SP-18301$_" allow users to record up to "_$cnet_capacity_blank_media$_" data" 
ELSE IF $cnet_capacity_blank_media$ IS NOT NULL
AND $SP-18301$ IS NOT NULL
THEN REF["SP-18301"].IfLike("Blu-Ray", "Blu-ray")_" allows users to record up to "_$cnet_capacity_blank_media$_" data" 
-- 4.7GB Storage Capacity 
ELSE IF $cnet_capacity_blank_media$ IS NOT NULL
THEN $cnet_capacity_blank_media$_" Storage Capacity" 
ELSE "@@";

--[FEATURE#4]
--Maximum video recording time (minutes)/Maximum audio recording time (minutes)
IF $SP-21987$ IS NOT NULL
OR $SP-21986$ IS NOT NULL
THEN COALESCE($SP-21987$, $SP-21986$)_" Minutes of recording space per disc" 
ELSE "@@";

-- [FEATURE #6]
-- Blank media package type

-- The Verbatim 95099 4.7 GB DVD-R Slim Jewel Case, 10/Pack, helps you to store and carry your important data, letting you access it from anywhere.
IF $SP-21992$ LIKE "Slim Jewel Case" 
THEN "Slim Jewel Case helps you to store and carry your important data" 
-- The DVDs come mounted on a convenient plastic spindle to keep dust off and prevent scratches. With the included plastic cover, they can be safely stored in any location
ELSE IF $SP-21992$ LIKE "Spindle" 
THEN "Spindle to keep dust off and prevent scratches" 
-- Protectively sealed in a moisture protective wrap for long term storage
ELSE IF $SP-21992$ LIKE "Wrap" 
THEN "Protectively sealed in a moisture protective wrap for long term storage" 
ELSE "@@";

--[FEATURE#8]
--Rewritable

--Disks allow you to record on them a single time, ensuring that your recorded material won't accidentally get deleted or overwritten
IF $SP-495$ LIKE "No" 
THEN "Disks allow you to record on them a single time ensuring that your recorded material won't accidentally get deleted or overwritten" 
--Re-write data and video on these discs ideal for archiving or back up. 
ELSE IF $SP-495$ LIKE "Yes" 
THEN "Rewrite data and video on these discs – ideal for archiving or backing up" 
ELSE "@@";

--[FEATURE #9]
--Media pack size (If more than 1)
IF $SP-12609$ NOT LIKE "Each" 
AND $SP-12609$ IS NOT NULL
THEN "Comes in "_Request.Data["TX_UOM"].Split("/").Last()_" of "_Request.Data["TX_UOM"].ExtractDecimals().First()
ELSE "@@";

--[FEATURE #EXTRA]

--They use advance AZO recording dye, which provides an extra layer of reliability and performance to archived materials
IF A[2408].Where("Metal AZO recording dye").Values IS NOT NULL
THEN "They use advance AZO recording dye, which provides an extra layer of reliability and performance to archived materials";

IF A[2408].Where("Metal Particle (MP)").Values IS NOT NULL
THEN "Base material: Metal Particle (MP) magnetic material";

IF A[2408].Where("NANOCUBIC nano-layer coating technology").Values IS NOT NULL
THEN "Features "_A[2408].Where("NANOCUBIC nano-layer coating technology").Values;

--Operating temperature range: 10 - 45 deg C, Day-to-day storage temperature:16 - 32 deg C, Long term storage temperature: 5 - 23 deg C
--Operating/non-operating temperature ranges from 5 to 40/-30 to 60 deg C and 10 - 90 percent humidity 
IF A[358].Value IS NOT NULL -- Min Operating Temperature
AND A[359].Value IS NOT NULL -- Max Operating Temperatur
AND A[360].Value IS NOT NULL -- Min Storage Temperature
AND A[361].Value IS NOT NULL -- Max Storage Temperature
AND A[362].Value IS NOT NULL -- humid
THEN "Operating temperature range: from " _$cnet_min_operating_temperature$_" to "_ $cnet_max_operating_temperature$_" degrees ##Fahrenheit; day-to-day storage temperature: from "_ $cnet_min_storage_temp$_" to "_ $cnet_max_storage_temp$_" degrees ##Fahrenheit, and "_A[362].Value_" humidity";

--[FEATURE #10] 
--Warranty information 
--X-year manufacturer limited warranty 
--Lifetime manufacturer limited warranty 
IF $SP-16$ LIKE "%year%" 
THEN A[430].Value.ExtractDecimals().First()_"-year manufacturer limited warranty" 
ELSE IF $SP-16$ LIKE "%Lifetime%" 
THEN "Lifetime manufacturer limited warranty" 
ELSE "@@"; 