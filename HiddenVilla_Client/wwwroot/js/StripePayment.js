redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51QPhNKDxa7Pb1xDwyvHQnbmusAlxyvTZix0RCV26tkqskhcnAcU3B22OMALRYo9Za1L0HHqH8Oj5K7tdG1AJGG0Y00k0ZO1tv1');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};