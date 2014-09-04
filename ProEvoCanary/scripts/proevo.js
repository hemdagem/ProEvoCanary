var ProEvo = ProEvo || {};

ProEvo.viewResult = function () {

    var playerOne = document.getElementById("playerOne");
    var playerTwo = document.getElementById("playerTwo");

    window.location = "/Records/HeadToHeadResults?playerOneId=" + playerOne.value + "&playertwoId=" + playerTwo.value;
};
