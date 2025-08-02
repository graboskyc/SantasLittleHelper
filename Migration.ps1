$inputPath = "C:\Users\chris\OneDrive\Desktop\santa.gifts.json"
$outputPath = "C:\Users\chris\OneDrive\Desktop\santa.gifts.lowercase.json"

# Read and parse the JSON
$json = Get-Content $inputPath -Raw | ConvertFrom-Json

function Lowercase-Emails($obj) {
    if ($obj -is [System.Collections.IEnumerable] -and -not ($obj -is [string])) {
        foreach ($item in $obj) { Lowercase-Emails $item }
    } elseif ($obj -is [PSCustomObject]) {
        foreach ($prop in $obj.PSObject.Properties) {
            if (($prop.Name -eq 'email' -or $prop.Name -eq 'purchasedBy') -and $prop.Value -is [string]) {
                if ($prop.Value -like '*@*') {
                    $prop.Value = $prop.Value.ToLower()
                }
            } else {
                Lowercase-Emails $prop.Value
            }
        }
    }
}

Lowercase-Emails $json

# Write the JSON back to file with correct formatting
$json | ConvertTo-Json -Depth 100 | Set-Content -Path $outputPath -Encoding UTF8