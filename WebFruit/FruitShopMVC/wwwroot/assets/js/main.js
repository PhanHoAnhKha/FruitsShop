(function ($) {
    "use strict";

    $(document).ready(function($){
        
        // testimonial sliders
        $(".testimonial-sliders").owlCarousel({
            items: 1,
            loop: true,
            autoplay: true,
            responsive:{
                0:{
                    items:1,
                    nav:false
                },
                600:{
                    items:1,
                    nav:false
                },
                1000:{
                    items:1,
                    nav:false,
                    loop:true
                }
            }
        });

        // homepage slider
        $(".homepage-slider").owlCarousel({
            items: 1,
            loop: true,
            autoplay: true,
            nav: true,
            dots: false,
            navText: ['<i class="fas fa-angle-left"></i>', '<i class="fas fa-angle-right"></i>'],
            responsive:{
                0:{
                    items:1,
                    nav:false,
                    loop:true
                },
                600:{
                    items:1,
                    nav:true,
                    loop:true
                },
                1000:{
                    items:1,
                    nav:true,
                    loop:true
                }
            }
        });

        // logo carousel
        $(".logo-carousel-inner").owlCarousel({
            items: 4,
            loop: true,
            autoplay: true,
            margin: 30,
            responsive:{
                0:{
                    items:1,
                    nav:false
                },
                600:{
                    items:3,
                    nav:false
                },
                1000:{
                    items:4,
                    nav:false,
                    loop:true
                }
            }
        });

        // count down
        if($('.time-countdown').length){  
            $('.time-countdown').each(function() {
            var $this = $(this), finalDate = $(this).data('countdown');
            $this.countdown(finalDate, function(event) {
                var $this = $(this).html(event.strftime('' + '<div class="counter-column"><div class="inner"><span class="count">%D</span>Days</div></div> ' + '<div class="counter-column"><div class="inner"><span class="count">%H</span>Hours</div></div>  ' + '<div class="counter-column"><div class="inner"><span class="count">%M</span>Mins</div></div>  ' + '<div class="counter-column"><div class="inner"><span class="count">%S</span>Secs</div></div>'));
            });
         });
        }

        // projects filters isotop
        $(".product-filters li").on('click', function () {
            
            $(".product-filters li").removeClass("active");
            $(this).addClass("active");

            var selector = $(this).attr('data-filter');

            $(".product-lists").isotope({
                filter: selector,
            });
            
        });
        
        // isotop inner
        $(".product-lists").isotope();

        // magnific popup
        $('.popup-youtube').magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });

        // light box
        $('.image-popup-vertical-fit').magnificPopup({
            type: 'image',
            closeOnContentClick: true,
            mainClass: 'mfp-img-mobile',
            image: {
                verticalFit: true
            }
        });

        // homepage slides animations
        $(".homepage-slider").on("translate.owl.carousel", function(){
            $(".hero-text-tablecell .subtitle").removeClass("animated fadeInUp").css({'opacity': '0'});
            $(".hero-text-tablecell h1").removeClass("animated fadeInUp").css({'opacity': '0', 'animation-delay' : '0.3s'});
            $(".hero-btns").removeClass("animated fadeInUp").css({'opacity': '0', 'animation-delay' : '0.5s'});
        });

        $(".homepage-slider").on("translated.owl.carousel", function(){
            $(".hero-text-tablecell .subtitle").addClass("animated fadeInUp").css({'opacity': '0'});
            $(".hero-text-tablecell h1").addClass("animated fadeInUp").css({'opacity': '0', 'animation-delay' : '0.3s'});
            $(".hero-btns").addClass("animated fadeInUp").css({'opacity': '0', 'animation-delay' : '0.5s'});
        });

       

        // stikcy js
        $("#sticker").sticky({
            topSpacing: 0
        });

        //mean menu
        $('.main-menu').meanmenu({
            meanMenuContainer: '.mobile-menu',
            meanScreenWidth: "992"
        });
        
         // search form
        $(".search-bar-icon").on("click", function(){
            $(".search-area").addClass("search-active");
        });

        $(".close-btn").on("click", function() {
            $(".search-area").removeClass("search-active");
        });
    
    });


    jQuery(window).on("load",function(){
        jQuery(".loader").fadeOut(1000);
    });


}(jQuery));


/* Khi lê chuột vào icon User */
  document.addEventListener("DOMContentLoaded", function() {
    const dropdown = document.querySelector('.dropdown');
    dropdown.addEventListener('mouseenter', function() {
      this.classList.add('active');
    });
    dropdown.addEventListener('mouseleave', function() {
      this.classList.remove('active');
    });
  });
/* Khi lê chuột vào icon User */


// Tổng số lượng + tiền khii nhấn //
function updateCart(input) {
    var quantity = input.value;
    var pricePerUnit = parseFloat(input.parentNode.previousElementSibling.textContent.replace('$', ''));
    var totalPrice = quantity * pricePerUnit;
    input.parentNode.nextElementSibling.textContent = totalPrice + '$';
    calculateTotal();
}

function calculateTotal() {
    var total = 0;
    var rows = document.querySelectorAll('.table-body-row');
    rows.forEach(function(row) {
        var totalPriceCell = row.querySelector('.product-total');
        var totalPrice = parseFloat(totalPriceCell.textContent.replace('$', ''));
        total += totalPrice;
    });
    // Cập nhật tổng số tiền vào ô tổng số tiền
    var totalAmountCell = document.getElementById('total-amount');
    totalAmountCell.textContent = total + '$';
    
    // Tính toán và cập nhật tổng số tiền cuối cùng
    var shippingFee = 30; // Phí vận chuyển cố định
    var finalTotalCell = document.getElementById('final-total');
    finalTotalCell.textContent = (total + shippingFee) + '$';
}
// Tổng số lượng + tiền khii nhấn //


// Lưu trữ số lượng và tổng tiền vào Session Storage
function updateCart(input) {
    // Code cập nhật số lượng và tổng tiền
    // Lưu giá trị vào Session Storage
    var quantity = input.value;
    var pricePerUnit = parseFloat(input.parentNode.previousElementSibling.textContent.replace('$', ''));
    var totalPrice = quantity * pricePerUnit;
    input.parentNode.nextElementSibling.textContent = totalPrice + '$';
    calculateTotal();

    // Lưu giá trị vào Session Storage
    sessionStorage.setItem('totalQuantity', quantity);
    sessionStorage.setItem('totalPrice', totalPrice);
}

// Cập nhật số lượng và tổng tiền từ Session Storage khi tải trang
function updateFromSessionStorage() {
    var totalQuantity = sessionStorage.getItem('totalQuantity');
    var totalPrice = sessionStorage.getItem('totalPrice');
    if (totalQuantity && totalPrice) {
        // Cập nhật số lượng và tổng tiền trên trang từ Session Storage
        // Đảm bảo gọi hàm tính toán tổng tiền sau khi cập nhật
    }
}

// Lưu trữ số lượng và tổng tiền vào Session Storage


// Xóa Sp //
function removeProduct(button) {
    var row = button.closest('.table-body-row');
    row.parentNode.removeChild(row);
    calculateTotal();
}
// Xóa Sp //


// Mã giảm giá //
document.addEventListener('DOMContentLoaded', function() {
    let appliedCoupons = []; // Mảng để theo dõi các mã giảm giá đã được áp dụng

    document.getElementById('apply-coupon-btn').addEventListener('click', function(event) {
        event.preventDefault();

        const couponCode = document.getElementById('coupon-code').value;

        if (appliedCoupons.includes(couponCode)) {
            alert('Mã giảm giá này đã được áp dụng');
            return;
        }

        // Giả sử bạn có một hàm để kiểm tra và áp dụng mã giảm giá
        if (isValidCoupon(couponCode)) {
            applyCouponDiscount(couponCode);
            appliedCoupons.push(couponCode); // Thêm mã giảm giá vào mảng đã áp dụng
        } else {
            alert('Mã giảm giá không hợp lệ');
        }
    });
});

function isValidCoupon(code) {
    // Kiểm tra xem mã giảm giá có hợp lệ hay không
    // Danh sách mã giảm giá hợp lệ và mức chiết khấu tương ứng
    const validCoupons = {
        'CNTT-K19': 0.1, // Giảm 10%
        'Em Anh Kha': 0.3 // Giảm 30%
    };
    return validCoupons.hasOwnProperty(code);
}

function applyCouponDiscount(code) {
    // Danh sách mã giảm giá hợp lệ và mức chiết khấu tương ứng
    const validCoupons = {
        'CNTT-K19': 0.1, // Giảm 10%
        'Em Anh Kha': 0.3, // Giảm 30%
    };

    // Lấy tổng số tiền hiện tại
    const currentTotal = parseFloat(document.getElementById('total-amount').textContent.replace('$', ''));
    
    // Giảm giá từ tổng số tiền
    const discountAmount = currentTotal * validCoupons[code];
    const finalTotal = currentTotal - discountAmount;

    // Cập nhật tổng số tiền và số tiền cuối cùng
    document.getElementById('final-total').textContent = `${finalTotal.toFixed(2)}$`;
}
// Mã giảm giá //






