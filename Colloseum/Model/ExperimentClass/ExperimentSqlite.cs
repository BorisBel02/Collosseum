using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using DB.Entity;
using DB.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Colloseum.Model;

public class ExperimentSqlite : IExperiment
{
    private IGods _gods;
    private IFighter _elon;
    private IFighter _mark;
    private DbContext _db;
    
    public ExperimentSqlite(
        IGods gods,
        IFighter elon,
        IFighter mark,
        DbContext db)
    {
        _gods = gods;
        _elon = elon;
        _mark = mark;
        _db = db;
    }
    public bool Run()
    {
        _gods.Shuffle();
        
        Array.Copy(_gods.GetDeck(), 0, _elon.FighterCards, 0, _gods.GetDeck().Length / 2);
        Array.Copy(_gods.GetDeck(), _gods.GetDeck().Length / 2, _mark.FighterCards,
            0, _gods.GetDeck().Length / 2);
        //well, maybe i could just use ArrayList instead of simple array...

        var cardEntityList = new List<CardEntity>();
        int index = 0;
        foreach (var card in _gods.GetDeck())
        {
            var cardEntity = CardMapper.MapCardEntity(card);
            cardEntity.Index = index;
            cardEntityList.Add(cardEntity);
            ++index;
        }

        var experimentEntity = new ExperimentEntity
        {
            Deck = cardEntityList
        };
        
        _db.Add(experimentEntity);
        
        return _elon.ChosenCard(_mark.ChooseNumber()).CardColour
               == _mark.ChosenCard(_elon.ChooseNumber()).CardColour;
    }
}