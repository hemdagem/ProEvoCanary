var ProEvo = ProEvo || {};

ProEvo.viewResult = function () {

    var playerOne = document.getElementById("playerOne");
    var playerTwo = document.getElementById("playerTwo");

    if (window.fetch !== undefined) {
        window.fetch('/Records/HeadToHeadResult', {
            body: "playerOne=" + playerOne + "&playerTwo=" + playerTwo
        })
                  .then(
                    function (response) {
                        if (response.status !== 200) {
                            console.log('Looks like there was a problem. Status Code: ' +
                              response.status);
                            return;
                        }

                        // Examine the text in the response  
                        response.json().then(function (data) {
                            console.log(data);
                        });
                    }
                  )
                  .catch(function (err) {
                      console.log('Fetch Error :-S', err);
                  });
    } else {
        window.location = "/Records/HeadToHeadResults?playerOneId=" + playerOne.value + "&playertwoId=" + playerTwo.value;
    }


};
