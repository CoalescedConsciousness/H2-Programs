const suites = [ "Hearts", "Clubs", "Spades", "Diamonds" ]
const cards = [ 2, 3, 4, 5, 6, 7, 8, 9, 10, "Jack", "Queen", "King", "Ace" ]
var deck = [];
var cardCount = document.getElementById("cardNum").value ? parseInt(document.getElementById("cardNum").value) : 52
var playerCount = document.getElementById("playerNum").value ? parseInt(document.getElementById("playerNum").value) : 1
var dealtCards = [];
var players = [];

function player() {
    name: ""
    score: 0
    hand: 0
}
function card() {
    suite: ""
    value: ""
}


function newGame()
{
    deck = buildDeck()
    deck = shuffleCards(deck)
    players = createPlayers()
    buildHtml();
}

function createPlayers()
{
    let players = [];
    for (let i = 0; i < parseInt(document.getElementById("playerNum").value); i++)
    {
        let nPlayer = new player()
        nPlayer.name = `Player ${i}`;
        players.push(nPlayer); 
    }
    return players
}


function buildDeck()
{
    deck = [];
    for (const x in suites)
    {
        
        for (let y = 0; y < cards.length; y++)
        {
            var aCard = new card();
            aCard.suite = suites[x]
            aCard.value = cards[y]
            deck.push(aCard)
        }
        
    }
    return deck;
}

function shuffleCards(deck)
{
    for (let i = 0; i < deck.length; i++)
    {
        let rndVal = randomNumberInRange(1, deck.length - 1)
        let tempCard = deck[rndVal]
        let origCard = deck[i]
        deck[i] = tempCard
        deck[rndVal] = origCard
    };
    return deck
}

function randomNumberInRange(fromInt, toInt)
{
    return Math.floor(Math.random() * toInt) + fromInt;
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