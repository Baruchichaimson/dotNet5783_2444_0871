﻿namespace BO;

    public enum CoffeeShop { COFFE_MACHINES, CAPSULES, ACCESSORIES, FROTHERS, SWEETS };
    public enum OrderStatus { CONFIRMED, SHIPPED, PROVIDED };
    public enum UserProduct { EXIT, LIST_REQUEST, DETAILS_REQUEST_ADMIN, DETAILS_REQUEST_CUSTOMER, ADDING_PRODUCT, PRODUCT_DELETION, UPDATE_DATA};
    public enum UserOrder { EXIT, LIST_REQUEST, ORDER_UPDATE_ADMIN, DETAILS_REQUEST, UPDATE_SHIPPING, UPDATE_DELIVERY, ORDER_TRACKING};
    public enum UserCart { EXIT , ADDING_PRODUCT , UPDATE_AMOUNT, ORDER_CONFIRMATION }
    public enum UserForMainBO { EXIT, PRODUCT, ORDER, CART };

