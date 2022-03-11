var allValues = 0;

// Helper function that toggles buttons in accordance with the currently active player.
function buttonShift()
{
    if (logging) console.log("Cycling player buttons")
    for (let i = 0; i < playerTurn.length; i++)
    {
        targetHit = document.getElementById(`hitPlayer${(playerTurn[i])}`)
        i != 0 ? targetHit.disabled = true : targetHit.disabled = false;

        targetStand = document.getElementById(`standPlayer${(playerTurn[i])}`)
        i != 0 ? targetStand.disabled = true : targetStand.disabled = false;
    }
}
// Helper function that translates card values to integers.
function getRealValue(cardValue)
{
    let value = 0;

    if (!isNaN(cardValue))
    {
        value = parseInt(cardValue)
    }
    else if (cardValue == "Ace")
    {
        value = 1 // Get back to this when appropriate!
    }
    else 
    {
        value = 10
    }

    return value
}
// Cycles through players.
function changePlayer()
{
    players[playerTurn[0]].hand = [];
        // Chaaange places!
        let validPlayer = false;
        while (!validPlayer)
        {
            if (playerTurn.length != 0)
            {
                lastPlayer = playerTurn.shift()
                playerTurn.push(lastPlayer)
                if (document.getElementById(`playerHold${playerTurn[0]}`) > 0)
                {
                    playerTurn.pop()
                }
                else 
                {
                    validPlayer = true;
                }
            }
            else
            {
                if (window.confirm("Start new game?")) newGame();
            }
        }
        
        
        document.getElementById("currentPlayer").innerText = playerTurn[0]
        buttonShift(); // Changes active buttons to correlate with current player.
}
// Function that iterates through a deck every time a new card is added to it, so ensure Aces are handled correctly.
function recalcScore(deck)
{
    if (logging) console.log("Recalculating")
    tempAceDeck = []
    let result = 0;
    for (item in deck)
    {

        let number = getRealValue(deck[item].value)
        if (number != 1)
        {
            result += number
        }      
        else tempAceDeck.push(number)
    }

    if (tempAceDeck.length > 1)
    {
        if (result < 10 - (tempAceDeck.length-1)) // 21 - 11 = 10, minus every Ace but one (x * 1); if this is more than 11 points from 21, convert 1 Ace to 11, add rest as 1
                                                  // I.e.: You'll never be able to add 2x11 anyway, but you have to account for up to 3 ones in addition to that
                                                  // so you can't blindly convert the first Ace in a collection to 11, if this would result in X + 11 + [1-3] > 21 
        {
            result += 11 // First Ace
            tempAceDeck.shift() // Remove it
            result += tempAceDeck.length // Add remaining Aces
        }
        else (result += tempAceDeck.length)
    }
    else if (tempAceDeck.length == 1)
    {
        if (result += 11 > 21) result += 1
        else result += 11
    }

    if (result != 21) result -= deck.find(x => x.holeCard == true) ? getRealValue(deck.find(x => x.holeCard == true).value) : 0
    
    return result
    
}
// [!] Deprecated score calculation function.
function calcScore(currentVal, value) 
{   
    console.log(value)
    if (value === 1)
    {
        if (currentVal + 11 <= 21) return currentVal + 11
        else return currentVal + 1 
    }
    else return currentVal + value;
}
// Helper function for generating a random number within a given range.
function randomNumberInRange(fromInt, toInt)
{
    return Math.floor(Math.random() * toInt) + fromInt;
}
// Checks if user input is a number, and disallows anything but those (and backspace)   --- consider moving to helper file.
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