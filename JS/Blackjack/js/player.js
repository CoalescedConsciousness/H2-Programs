const playerSection = document.getElementById("playerSection");
var counter;

// Programmatically constructs Player board(s).
function buildHtml()
{
    // Resets board if needed
    if (playerSection.hasChildNodes())
    {
        while (playerSection.hasChildNodes())
        {
            playerSection.removeChild(playerSection.firstChild)
        }
    }

    // Builds board
    for (item in players)
    {
        counter = parseInt(item) // +1
        let playerBoard = document.createElement('div')
        playerBoard.setAttribute("id", "playerContainer");
        playerBoard.setAttribute("name", "playerCon");
        playerBoard.setAttribute("class", "col-sm-12");
        playerBoard.style.margin = "2%";
        playerBoard.style.height = "auto";
        playerBoard.style.width = "20%";
        playerSection.appendChild(playerBoard)
        
        // Table
        let table = document.createElement('table')
        table.style.border = "2px solid lightgray"
        table.setAttribute("class", "col-sm-11")
        table.style.margin = "20px";
        playerBoard.appendChild(table)

        
        // Header rows
        let headerRow = table.appendChild(document.createElement('thead'))
       
        headerCellOne = headerRow.appendChild(document.createElement('td'))
        headerCellOne.appendChild(document.createTextNode('Player:'))
        headerCellTwo = headerRow.appendChild(document.createElement('td'))
        headerCellTwo.appendChild(document.createTextNode('Hand:'))
        headerCellThree = headerRow.appendChild(document.createElement('td'))
        headerCellThree.appendChild(document.createTextNode('Score:'))
        
        // Data rows
        let dataRow = table.appendChild(document.createElement('tbody'))

        dataCellOne = dataRow.appendChild(document.createElement('td'))
        input = document.createElement('input')
        input.style.width = "60px"
        input.setAttribute("id", `playerName${counter}`)
        input.setAttribute("placeholder", (players[counter].name ? players[counter].name : `P${counter}`))
        dataCellOne.appendChild(input)

        dataCellTwo = dataRow.appendChild(document.createElement('td'))
        hand = document.createElement('p')
        hand.style.width = "100px"
        hand.setAttribute("id", `playerHand${counter}`)
        hand.setAttribute("placeholder", "0")
        dataCellTwo.appendChild(hand)

        dataCellThree = dataRow.appendChild(document.createElement('td'))
        score = document.createElement('p')
        score.style.width = "50px"
        score.setAttribute("id", `playerScore${counter}`)
        score.setAttribute("placeholder", "0")
        score.innerText = 0;
        dataCellThree.appendChild(score)

        // Buttons
        let hitButton = document.createElement('button')
        hitButton.setAttribute("onclick", "playerHit()")
        hitButton.textContent = "Hit"
        hitButton.style.margin = "10px"
        hitButton.style.padding = "10px"
        hitButton.style.width = "300px"
        hitButton.setAttribute("id", `hitPlayer${counter}`)
        hitButton.setAttribute("class", "player-button-hit")
        playerBoard.appendChild(hitButton)

        let standButton = document.createElement('button')
        standButton.setAttribute("onclick", "playerStand()")
        standButton.textContent = "Stand"
        standButton.style.margin = "10px"
        standButton.style.width = "auto"
        standButton.style.padding = "10px"
        standButton.setAttribute("class", "player-button-stand")
        standButton.id = `standPlayer${counter}`
        playerBoard.appendChild(standButton)

        // Player turn counter:
        let pTurnElement = document.getElementById("currentPlayer")
        pTurnElement.innerText = playerTurn[0] + 1;
        
        // Player holdings:
        let pHolding = document.createElement("table")
        pRow = document.createElement("thead")
        pHolding.appendChild(pRow)
        pHeaderText = document.createElement("td")
        pHeaderText.style.width = "80px"
        pHeaderText.textContent = "Holding:"
        pRow.appendChild(pHeaderText)
        pHeaderValue = document.createElement("td")
        pHeaderValue.style.width = "50%"
        pHeaderValue.textContent = players[item].holding
        pHeaderValue.setAttribute("id", `playerHold${counter}`)
        pRow.appendChild(pHeaderValue)
        playerBoard.appendChild(pHolding)

        pBody = document.createElement("tbody")
        pHolding.appendChild(pBody)
        pBetText = document.createElement("td")
        pBetText.style.width = "80px"
        pBetText.textContent = "Bet:"
        pBody.appendChild(pBetText)
        pBetValue = document.createElement("td")
        pBetValue.style.width = "240px"
        pBetValue.setAttribute("id", `playerBet${counter}`)
        pBetValue.textContent = players[item].bet
        pBody.appendChild(pBetValue)
        playerBoard.appendChild(pHolding)
    }
}
// Player object:
class player {
    constructor(name, score, hand, holding, bet)
    {
        this._name = name,
        this._score = score,
        this._hand = hand,
        this._holding = holding,
        this._bet = bet
    }
}
// Function called when the player hits the corresponding "Hit" button (only one can be active at a time).
function playerHit()
{
    console.log(playerTurn)
    pTarget = document.getElementById(`playerHand${playerTurn[0]}`)
    newHit = document.createElement("p")
    pTarget.appendChild(newHit)
    let hit = deck.pop()
    console.log("Hi")
    console.log(players)
    console.log(players[playerTurn[0]])
    players[playerTurn[0]].hand.push(hit)
 
    newHit.appendChild(document.createTextNode("["+hit.value+" of "+hit.suite+"]"))
    playerScore() 
}
// Updates the player score when a given player receives a card, calls recalcScore in the process.
function playerScore()
{
    sTarget = document.getElementById(`playerScore${playerTurn[0]}`)
    // currentVal = parseInt(sTarget.innerText)
    // console.log(typeof(currentVal))
    // console.log(players[playerTurn[0]].hand)
    score = recalcScore(players[playerTurn[0]].hand)

    players[playerTurn[0]].score = score;
    sTarget.innerText = score;

    if (score > 21) endTurn(true);
    if (players[playerTurn[0]].hand.length == 2 && score == 21) endTurn(null)
    score = 0;
}
// Seperate function for the sake of clarity; simply calls the endTurn function with an argument that indicates the player has chosen to stand.
function playerStand()
{
    endTurn(false)
}
// Creates a number of players dependent to the value given in the settings.
function createPlayers()
{
    players = [];
    for (let i = 0; i < parseInt(document.getElementById("playerNum").value); i++)
    {
        let nPlayer = new player();
        nPlayer.name = `Player ${i}`;
        nPlayer.score = 0;
        nPlayer.hand = [];
        nPlayer.holding = 100;
        nPlayer.bet = 0;
        players.push(nPlayer); 
    }
    console.log("ARGH")
    console.log(players)
}

