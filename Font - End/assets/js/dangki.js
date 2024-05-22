document.querySelector("#form-dangki form").addEventListener("submit", function(event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của biểu mẫu

    // Lấy giá trị từ các trường nhập liệu
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var confirm_password = document.getElementById("confirm_password").value;

    // Kiểm tra xem các trường nhập liệu có được điền đầy đủ không
    if (username === "" || email === "" || password === "" || confirm_password === "") {
        alert("Hãy nhập thông tin đầy đủ!");
    } else if (password !== confirm_password) {
        alert("Mật khẩu và xác nhận mật khẩu không khớp!");
    } else {
        // Hiển thị thông báo đăng ký thành công
        alert("Đăng ký thành công!");

        // Xóa nội dung của các trường nhập liệu sau khi đăng ký thành công
        document.getElementById("username").value = "";
        document.getElementById("email").value = "";
        document.getElementById("password").value = "";
        document.getElementById("confirm_password").value = "";
    }
});
