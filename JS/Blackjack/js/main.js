const suites = [ "Hearts", "Clubs", "Spades", "Diamonds" ]
const cards = [ 2, 3, 4, 5, 6, 7, 8, 9, 10, "Jack", "Queen", "King", "Ace" ]
var deck = [];
var cardCount = document.getElementById("cardNum").value ? parseInt(document.getElementById("cardNum").value) : 52
var playerCount = document.getElementById("playerNum").value ? parseInt(document.getElementById("playerNum").value) : 1
var players = [];
var playerTurn = playersToArray()
var gameDealer;

// Function to help determine amount of players.
function playersToArray()
{
    let arrPlayers = []
    for (let i = 0; i < playerCount; i++) arrPlayers.push(i)
    return arrPlayers
}


// Starts new game by building deck > shuffling it > creating players > building player boards > initializing player positions (=> Player 1)
function newGame()
{
    deck = buildDeck()
    deck = shuffleCards(deck)
    players = createPlayers()
    buildHtml();
    buttonShift();
}

// Ends turn depending on player choice/game result. Trinary result: 
// 1) player stood, dealers turn (bust == false)
// 2) player won or got blackjack (bust == null)
// 3) player lost or went bust (bust == true)
function endTurn(bust)
{
    console.log("Turn ending")
    sTarget = document.getElementById(`playerScore${playerTurn[0]}`)
    pTarget = document.getElementById(`playerHand${playerTurn[0]}`)

    if (bust == false) // Player Stood
    {
        console.log("Sending to Dealer")
        dealerTurn()
        
    }
    if (bust == null) // Player got blackjack/won
    {
        console.log("Wahey!")
        alert("You won!")
        changePlayer()
    }
    if (bust == true) // Player busted/lost
    {
        console.log("Aww..")
        alert("You lost!")
        changePlayer()
    }
    console.log(playerTurn[0])
    // Clear hand and score:
    sTarget.innerText = 0
    pTarget.innerText = ""
    
    // Clear Dealer:
    document.getElementById("dealerHand").innerText = ""
    document.getElementById("dealerScore").innerText = 0



}
// Function that iterates through the dealers turn automatically.
function dealerTurn()
{
    let dealerDeck = [];
    let result = 0;
    while (result < 16)
    {
        console.log("Dealing to Dealer")
        let dealerHit = deck.pop()
        pTarget = document.getElementById(`dealerHand`)
        newHit = document.createElement("p")
        pTarget.appendChild(newHit)
        newHit.appendChild(document.createTextNode("["+dealerHit.value+" of "+dealerHit.suite+"]"))
        console.log("Test")
        dScore = document.getElementById(`dealerScore`)
        
        dealerDeck.push(dealerHit)
        result = recalcScore(dealerDeck)
        dScore.innerText = result
        console.log(result)
    }

    if (result > 21 || result < players[playerTurn[0]].score) endTurn(null)
    else if (result > players[playerTurn[0]].score || result == players[playerTurn[0]].score) endTurn(true)

}
