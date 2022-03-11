const suites = [ "Hearts", "Clubs", "Spades", "Diamonds" ]
const cards = [ 2, 3, 4, 5, 6, 7, 8, 9, 10, "Jack", "Queen", "King", "Ace" ]

var logging = document.getElementById("logAll").checked
var deck = [];
// var cardCount = document.getElementById("cardNum").value ? parseInt(document.getElementById("cardNum").value) : 52
var playerCount = parseInt(document.getElementById("playerNum").value);
var players = [];
var playerTurn = [];
var gameDealer;
var dealerDeck = [];
var gameRuleset = new ruleset();
var secondRound = false;
var affectingRule = document.getElementById("ruleSet").value;
updatePlayerNum();

document.addEventListener('keydown', increaseBet);

// Saves players into an array to allow for cycling players.
function playersToArray()
{
    playerTurn = [];
    if (logging) console.log(`Player Count: ${playerCount}`)
    for (let i = 0; i < playerCount; i++) playerTurn.push(i)
}
function updatePlayerNum()
{
    playerCount = parseInt(document.getElementById("playerNum").value)
    playersToArray();
}
// Starts new game by building deck > shuffling it > creating players > building player boards > initializing player positions (=> Player 1)
function newGame()
{
    console.log(affectingRule)
    endTurn("reset")
    dealerDeck = [];
    deck = buildDeck()
    deck = shuffleCards(deck)
    createPlayers()
    buildHtml();
    applyRules();
    buttonShift();
}
function nextTurn()
{
    dealerDeck = [];
    changePlayer();
    secondRound = false;
    applyRules();
}
// Ends turn depending on player choice/game result. Trinary result: 
// 1) player stood, dealers turn (bust == false)
// 2) player won or got blackjack (bust == null)
// 3) player lost or went bust (bust == true)
function endTurn(bust)
{
    if (logging) console.log("Turn ending")
    if (bust != "reset")
    {
        pScore = document.getElementById(`playerScore${playerTurn[0]}`)
        pHand = document.getElementById(`playerHand${playerTurn[0]}`)
        pHolding = document.getElementById(`playerHold${playerTurn[0]}`)
        pBet = document.getElementById(`playerBet${playerTurn[0]}`)
        
        if (bust == false) // Player Stood
        {
            console.log("Sending to Dealer")
            dealerTurn()
            
        }
        if (bust == null) // Player got blackjack/won
        {
            console.log("Wahey!")
            alert("You won!")
            pHolding.innerText = parseInt(pHolding.innerText) + (1.5 * parseInt(pBet.innerText))
            pBet.innerText = 0;
            // Clear Dealer:
            document.getElementById("dealerHand").innerText = ""
            document.getElementById("dealerScore").innerText = 0
            nextTurn();
        }
        if (bust == true) // Player busted/lost
        {
            console.log("Aww..")
            alert("You lost!")
            pHolding.innerText = parseInt(pHolding.innerText) - parseInt(pBet.innerText)
            pBet.innerText = 0;
            // Clear Dealer:
            document.getElementById("dealerHand").innerText = ""
            document.getElementById("dealerScore").innerText = 0
            nextTurn();

        }
        // Clear hand and score:
        pScore.innerText = 0
        pHand.innerText = ""
        // Clear Dealer:
    }
    if (bust == 'reset')
    {
        // Clear Dealer:
        document.getElementById("dealerHand").innerText = ""
        document.getElementById("dealerScore").innerText = 0
    }


}

// Applies rules based on player selection
function applyRules()
{
    rule = document.getElementById("ruleSet").value ? document.getElementById("ruleSet").value : "classic"
    console.log(rule)
    switch (rule)
    {
        case "classic":
            {
                dealerHit(true)
                dealerHit(false)
                playerHit()
                playerHit()
            }
            break;
        case "european":
            {
                if (secondRound != "over")
                {
                    if (secondRound == false)
                    {
                        dealerHit(false)
                        playerHit()
                        playerHit()
                        secondRound = true;
                    }
                    else if (secondRound == true)
                    {
                        dealerHit(true)
                        secondRound == "over";
                    }
                }
            }
    }
}
function dealerHit(hide)
{
    if (logging) console.log("Dealer hit:")
    let dealerCard = deck.pop()
    pTarget = document.getElementById(`dealerHand`)
    newHit = document.createElement("p")
    pTarget.appendChild(newHit)
    if (hide)
    {
        console.log(hide)
        newHit.setAttribute("id", "holeCard")
        holeData = document.createTextNode("[ Hidden ]")
        dealerCard.holeCard = true;
        dealerDeck.push(dealerCard)
        newHit.appendChild(holeData)
    }
    else if (!hide)
    {
        newHit.appendChild(document.createTextNode("["+dealerCard.value+" of "+dealerCard.suite+"]"))
        dealerDeck.push(dealerCard)
    }
    dScore = document.getElementById(`dealerScore`)
    console.log(dealerDeck)
    result = recalcScore(dealerDeck, true)
    console.log(result)
    dScore.innerText = result
    return result
}
// Function that iterates through the dealers turn automatically.
function dealerTurn()
{
    let result = 0;
    while (result < 16)
    {
        console.log("Dealing to Dealer")
        result = dealerHit()
        
    }

    if (result > 21 || result < players[playerTurn[0]].score) 
    {
        endTurn(null)
    }
    else if (result > players[playerTurn[0]].score || result == players[playerTurn[0]].score) 
    {
        endTurn(true)
    }

}
function increaseBet(e)
{
    
    pHolding = document.getElementById(`playerHold${playerTurn[0]}`)
    pBet = document.getElementById(`playerBet${playerTurn[0]}`)
    
    if (e.code == "ArrowUp" || e.code == "KeyW") pBet.innerText = parseInt(pBet.innerText) + 5;
    if (e.code == "ArrowDown" || e.code == "KeyS") pBet.innerText = parseInt(pBet.innerText) - 5

    // This was just for fun; it's a double nested if-statement, so the player can never bet more than they have (pBet > pHolding), and less than 0 (pBet < 0)
    pBet.innerText = parseInt(pBet.innerText) > parseInt(pHolding.innerText) ? parseInt(pHolding.innerText) : parseInt(pBet.innerText) < 0 ? 0 : parseInt(pBet.innerText) 

    if (e.code == "Enter" || e.code == "KeyF") playerHit()
    if (e.code == "Backspace" || e.code == "Space") playerStand() 


}
// Load and Save data
function saveGameState()
{
    console.log(players)
    let data = JSON.stringify(players)
    var file = URL.createObjectURL(new Blob([data], {type: "application/json"}))
    var saveBtn = document.getElementById("saveGame")
    saveBtn.setAttribute("href", file)
}