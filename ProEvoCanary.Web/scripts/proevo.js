var ProEvo = function() {
    var self = this;
    self.ViewResult = function () {

        var playerOne = document.getElementById("playerOne");
        var playerTwo = document.getElementById("playerTwo");

        window.location = "/Records/HeadToHeadResults?playerOneId=" + playerOne.value + "&playertwoId=" + playerTwo.value;
    };
    return self;
}

var proEvo = new ProEvo();
proEvo.ViewResult();

