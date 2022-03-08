// Card object
function card() {
    suite: ""
    value: ""
}
// Constructs a deck using the 2 constant arrays "suites" and "cards"
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
// Shuffles the constructed deck by iterating through the deck (x), selecting a random spot (y)
// holding the Y card in a temporary variable, assigning X card to position Y, and Y card to Position X
// Resulting in a randomized deck
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