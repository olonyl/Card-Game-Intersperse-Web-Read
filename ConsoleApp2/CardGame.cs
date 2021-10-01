using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class CardGame
    {

        public static List<string> cards = new List<string>{
    "ace",
    "two",
    "three",
    "four",
    "five",
    "six",
    "seven",
    "eight",
    "nine",
    "ten",
    "jack",
    "queen",
    "king"
  };
        public static string ArrayChallenge(string[] strArr)
        {
            var totalCards = 0;
            List<int> aces = new List<int>();
            //List<int> faces = new List<int>();
            var maxCard = 0;
            //Split cards based on their relevance
            foreach (var card in strArr)
            {
                var cardValue = cards.IndexOf(card) + 1;
                //var cardGameValue = cardValue >= 11? 10:cardValue; 
                if (cardValue == 1)
                    aces.Add(1);
                else if (cardValue >= 11)
                    totalCards += 10;
                else totalCards += cardValue;
                //Capture the highest card
                if (cardValue > maxCard) maxCard = cardValue;
            }

            //Return the message in case the total of common cards reaches out 21
            var strMaxCard = cards[maxCard - 1];
            if (totalCards > 21) return $"above {strMaxCard}";
            foreach (var card in aces)
            {
                if (totalCards + 11 > 21) totalCards += 1;
                else totalCards += 11;
                if (totalCards > 21) return $"above {strMaxCard}";
            }
            if (totalCards < 21) return $"below {strMaxCard}";
            else return "blackjack ace";

        }

        public static void Call()
        {
            // keep this function call here
            Console.WriteLine(ArrayChallenge(new string[] {"ace","queen" }));
        }
    }
}
