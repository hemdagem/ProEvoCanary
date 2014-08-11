var ProEvo = ProEvo || {};

ProEvo.viewResult = function() {

    var elements = document.getElementsByTagName("select");

    if (elements.length === 2) {
        window.location = "/Records/HeadToHeadResults?playerOneId=" + elements[0].value + "&playertwoId=" + elements[1].value;
    }
};
