// Tải dữ liệu mẫu
using MlApp;

var sampleData = new MLModel1.ModelInput()
{
	ProductId = 2F,
	ProductName = @"Táo",
	ProductDescription = @"Táo Ninh Thuận, nổi tiếng với hình dáng tròn hoặc hơi dài, vỏ mỏng màu xanh sáng khi non và chuyển sang vàng nhạt khi chín, có kích thước vừa phải, bề mặt láng mịn, thịt quả giòn, ngọt và thơm mát, là đặc sản đặc trưng của vùng đất Ninh Thuận, Việt Nam, nơi có khí hậu khô hạn và nắng nhiều, tạo điều kiện lý tưởng cho loại quả này phát triển với hương vị đặc biệt và chất lượng cao.",
	ImageUrl = @"https://pbs.twimg.com/media/GO3lBF1asAAVXCK?format=png&name=900x900",
	IsSaling = true,
};

// Tải mô hình và dự đoán kết quả
var result = MLModel1.Predict(sampleData);
var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";
Console.WriteLine($"Text: {sampleData.ProductName}\nSentiment: {sentiment}");
