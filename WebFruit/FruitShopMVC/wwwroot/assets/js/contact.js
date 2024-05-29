document.querySelector(".contact-form form").addEventListener("submit", function(event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của biểu mẫu

    // Lấy giá trị từ các trường nhập liệu
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var phone = document.getElementById("phone").value;
    var subject = document.getElementById("subject").value;
    var message = document.getElementById("message").value;

    // Kiểm tra xem các trường nhập liệu có được điền đầy đủ không
    if (name === "" || email === "" || phone === "" || subject === "" || message === "") {
        alert("Hãy nhập thông tin đầy đủ!");
    } else {
        // Hiển thị thông báo gửi thành công
        alert("Đã gửi thành công!");

        // Xóa nội dung của các trường nhập liệu sau khi gửi thành công
        document.getElementById("name").value = "";
        document.getElementById("email").value = "";
        document.getElementById("phone").value = "";
        document.getElementById("subject").value = "";
        document.getElementById("message").value = "";
    }
});
