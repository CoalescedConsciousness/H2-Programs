const suites = [ "Hearts", "Clubs", "Spades", "Diamonds" ]
const cards = [ 2, 3, 4, 5, 6, 7, 8, 9, 10, "Jack", "Queen", "King", "Ace" ]
var deck = [];
var cardCount = document.getElementById("cardNum").value ? document.getElementById("cardNum").value : 52
var playerCount = document.getElementById("playerNum").value ? document.getElementById("playerNum").value : 1
var dealtCards = [];

function player() {
    name: ""
    score: 0
    hand: 0
}
function card() {
    suite: ""
    value: ""
}


function beginGame()
{
    deck = shuffle(cardCount)
    console.log(deck)
}

function shuffle(range)
{
    deck = [];
    for (const x in suites)
    {
        var aCard = new card();
        aCard.suite = x
        for (let y = 0; y < cards.length; y++)
        {
            aCard.value = cards[y]
            deck.push(aCard)
            
            // let rndVal = randomNumberInRange(1, cards.length)
            // if (!dealtCards.includes(rndVal))
            // {
            //     dealtCards.push(rndVal)
            // }
            // // let dealt = false;
            // // while (!dealt)
            // // {
            // //     console.log(cards.length)
            // //     let val = randomNumberInRange(1, cards.length)
            // //     dealtCards.push(val)
            // //     console.log(dealtCards)
            // //     dealt = true;
            // //     // aCard.value = cards[randomNumberInRange(1, cards.length)]
            // //     // console.log(dealtCards.includes(aCard))
            // //     // if (dealtCards.includes(aCard.value) == false)
            // //     // {
            // //     //     dealtCards.push(aCard.value)
            // //     //     deck.push(aCard)
            // //     //     dealt = true;
            // //     // }
            // // }
        }

        // for (item in deck)
        // {
        //     let rndVal = randomNumberInRange(1, cards.length)
        //     let tempCard = deck[rndVal]
            
        //     deck[rndVal] = card

        // }
    }
    return deck;
}

function randomNumberInRange(fromInt, toInt)
{
    let arrRes = [];
    arrRes.push(result = Math.floor(Math.random() * toInt) + fromInt);
    console.log(result)

}

function checkNumInput(event)
{
    console.log(event)
    if (event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode >= 96 && event.keyCode <= 105 || event.keyCode == 8)  // Top row numbers
    {
        return window.event.returnValue = true;
    }
    else {
        return window.event.returnValue = false;
    }
}