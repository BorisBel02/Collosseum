using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using Microsoft.Extensions.Hosting;

namespace Colloseum.Model;

public class Experiment
{
   private Deck.Gods _gods;

   private Fighter _Elon;
   private Fighter _Mark;

   public Experiment()
   {
      _gods = Deck.Gods.GetInstance();

      _Elon = new Fighter();
      _Mark = new Fighter();
   }

   public bool Run()
   {
      _gods.Shuffle();
      
      Array.Copy(_gods.GetDeck(), 0, _Elon.FighterCards, 0, _gods.GetDeck().Length / 2);
      Array.Copy(_gods.GetDeck(), _gods.GetDeck().Length / 2, _Mark.FighterCards,
         0, _gods.GetDeck().Length / 2);

      return _Elon.ChosenCard(_Mark.ChooseNumber()).CardColour
             == _Mark.ChosenCard(_Elon.ChooseNumber()).CardColour;
   }
}