using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [CreateAssetMenu(fileName = "Advanced Rule Tile", menuName = "2D/Tiles/Advanced Rule Tile", order = 4)]
    public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor> 
    {
        public bool AlwaysConnect;
        public bool CheckSelf;
        public TileBase[] TilesToConnect;
        
        public class Neighbor : RuleTile.TilingRule.Neighbor 
        {
            public const int Any = 3;
            public const int Specified = 4;
            public const int Nothing = 5;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor) 
            {
                case Neighbor.This: return CheckThis(tile);
                case Neighbor.NotThis: return CheckNotThis(tile);
                case Neighbor.Any: return CheckAny(tile);
                case Neighbor.Specified: return CheckSpecified(tile);
                case Neighbor.Nothing: return CheckNothing(tile);
            }
            return base.RuleMatch(neighbor, tile);
        }

        private bool CheckThis(TileBase tile)
            => !AlwaysConnect ? tile == this : TilesToConnect.Contains(tile) || tile == this;
        
        private bool CheckNotThis(TileBase tile) 
            => tile != this;

        private bool CheckAny(TileBase tile)
            => tile != null; /* CheckSelf ? tile != null : tile != null && tile != this; */
        
        private bool CheckSpecified(TileBase tile)
            => TilesToConnect.Contains(this);
        
        private bool CheckNothing(TileBase tile) 
            => tile == null;
    }
}