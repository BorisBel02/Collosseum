using Colloseum.Model.Deck;
using DB.Entity;

namespace DB.Mapper;

public class CardMapper
{
    public static CardEntity MapCardEntity(Card card)
    {
        var entity = new CardEntity
        {
            CardSuit = card.CardSuit,
            CardValue = card.CardValue,
            CardColour = card.CardColour
        };
        
        return entity;
    }

    public static Card MapCard(CardEntity cardEntity)
    {
        return new Card(cardEntity.CardSuit, cardEntity.CardValue);
    }
}