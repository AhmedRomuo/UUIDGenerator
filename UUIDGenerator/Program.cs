using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UUIDGenerator.Services;

Console.WriteLine("DateTime UTC:" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));

//var dto = File.ReadAllText("test.json");
//JObject receipt = JObject.Parse(dto);


//// Serialize & compute UUID
//string uuid = EtaSerializer.ComputeUUID(receipt);

//var ss = EtaSerializer.Serialize(receipt);

//// Output
//Console.WriteLine("Serialized JSON:\n" + receipt.ToString());
//Console.WriteLine("\nSerialized for Hash:\n" + EtaSerializer.Serialize(receipt));
//Console.WriteLine("\nGenerated UUID:\n" + uuid);


//using UUIDGenerator.Services;

//string json = "{\r\n  \"header\":{\r\n    \"dateTimeIssued\":\"2025-11-30T20:18:25Z\",\r\n    \"receiptNumber\":\"4000000010404260\",\r\n    \"uuid\":\"\",\r\n    \"previousUUID\":\"\",\r\n    \"referenceOldUUID\":\"\",\r\n    \"currency\":\"EGP\",\r\n    \"exchangeRate\":0.0,\r\n    \"sOrderNameCode\":\"\",\r\n    \"orderdeliveryMode\":\"FC\",\r\n    \"grossWeight\":0.0,\r\n    \"netWeight\":0.0\r\n  },\r\n  \"documentType\":{\r\n    \"receiptType\":\"S\",\r\n    \"typeVersion\":\"1.2\"\r\n  },\r\n  \"seller\":{\r\n    \"rin\":\"506507882\",\r\n    \"companyTradeName\":\"توفير للمواد الغذائيه كازيون\",\r\n    \"branchCode\":\"472\",\r\n    \"branchAddress\":{\r\n      \"country\":\"EG\",\r\n      \"governate\":\"giza\",\r\n      \"regionCity\":\"pyramids\",\r\n      \"street\":\"4 ش الأمراء التعاون فيصل الهرم \",\r\n      \"buildingNumber\":\"4\",\r\n      \"postalCode\":\"\",\r\n      \"floor\":\"\",\r\n      \"room\":\"\",\r\n      \"landmark\":\"\",\r\n      \"additionalInformation\":\"\"\r\n    },\r\n    \"deviceSerialNumber\":\"202040\",\r\n    \"syndicateLicenseNumber\":\"\",\r\n    \"activityCode\":\"4721\"\r\n  },\r\n  \"buyer\":{\r\n    \"type\":\"P\",\r\n    \"id\":\"\",\r\n    \"name\":\"\",\r\n    \"mobileNumber\":\"\",\r\n    \"paymentNumber\":\"\"\r\n  },\r\n  \"itemData\":[\r\n    {\r\n      \"internalCode\":\"000000000111000324\",\r\n      \"description\":\"جهينة زبادى طبيعى 105جم\",\r\n      \"itemType\":\"GS1\",\r\n      \"itemCode\":\"6223000351321\",\r\n      \"unitType\":\"EA\",\r\n      \"quantity\":1.0,\r\n      \"unitPrice\":10.95,\r\n      \"netSale\":10.95,\r\n      \"totalSale\":10.95,\r\n      \"total\":10.95,\r\n      \"commercialDiscountData\":[\r\n        {\r\n          \"amount\":0.0,\r\n          \"description\":\"bbb\"\r\n        }\r\n      ],\r\n      \"itemDiscountData\":[\r\n        {\r\n          \"amount\":0.0,\r\n          \"description\":\"ttt\"\r\n        }\r\n      ],\r\n      \"valueDifference\":0.0,\r\n      \"taxableItems\":[{\r\n                                \"taxType\": \"T1\",\r\n                                \"amount\":  0.0 ,\r\n                                \"subType\": \"V009\",\r\n                                \"rate\": 0.0\r\n                        }\r\n      ]\r\n    }\r\n  ],\r\n  \"totalSales\":16.45,\r\n  \"totalCommercialDiscount\":0.0,\r\n  \"totalItemsDiscount\":0.0,\r\n  \"extraReceiptDiscountData\":[\r\n               {\r\n                   \"amount\": 0.0,\r\n                   \"description\": \"ABC\"\r\n               }\r\n            ],\r\n  \"netAmount\":16.45,\r\n  \"feesAmount\":0.0,\r\n  \"totalAmount\":16.45,\r\n  \"taxTotals\":[\r\n                    {\r\n                        \"taxType\": \"T1\",\r\n                        \"amount\": 0.0\r\n                    }\r\n            ],\r\n  \"paymentMethod\":\"C\",\r\n  \"adjustment\":0.0,\r\n  \"contractor\":{\r\n  },\r\n  \"beneficiary\":{\r\n  }\r\n}";
//var (normalized, uuidHex) = ReceiptUuidGenerator.GenerateReceiptUuid(json);
//Console.WriteLine("Normalized string (single line):");
//Console.WriteLine(normalized);
//Console.WriteLine();
//Console.WriteLine("Receipt UUID (sha256 hex): " + uuidHex);

//var settings = new JsonLoadSettings
//{
//    CommentHandling = CommentHandling.Ignore,
//    LineInfoHandling = LineInfoHandling.Ignore
//};

//string jsonText = System.IO.File.ReadAllText("test.json");


//var reader = new JsonTextReader(new StringReader(jsonText))
//{
//    DateParseHandling = DateParseHandling.None
//};

//JObject json = JObject.Load(reader);

////JObject json = JObject.Parse(jsonText, settings);

//string uuid = UUIDGenerator.Services.EtaSerializer.ComputeUUID(json);

//Console.WriteLine("Generated UUID:");
//Console.WriteLine(uuid);

string json = System.IO.File.ReadAllText("test.json").TrimStart('\uFEFF');

// Load JSON EXACTLY (preserves formatting)
JObject obj = JsonExactLoader.LoadJsonExact(json);

// Clear UUID
if (obj["header"]?["uuid"] != null)
    obj["header"]["uuid"] = "";

// Generate normalized string
string normalized = EtaSerializer.Serialize(obj);

// Print normalized text
Console.WriteLine("=== Normalized Text ===");
Console.WriteLine(normalized);

// Print UUID
string uuid = UuidGenerator.ComputeUUID(json);
Console.WriteLine("\n=== UUID ===");
Console.WriteLine(uuid);