using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gravityProject
{
    public class Services
    {
        private readonly GamePhysics _gamePhysics;
        private readonly AnimationManager _animationManager;

        public Services(GamePhysics gamePhysics, AnimationManager animationManager)
        {
            _gamePhysics = gamePhysics;
            _animationManager = animationManager;
        }

        public void PhysicsService(List<Ground> ground, Player player, List<EnemyCollider> enemyColliders, SoundEffect coinSound)
        {
            _gamePhysics.playerIntersectsWithCoins(coinSound);
            _gamePhysics.PlayerIntersectsWithChest();
            _gamePhysics.playerHealing();
            _gamePhysics.PlayerIntersectsWithTrap();
            _gamePhysics.PlayerIntersectsWithGround(ground, player.isFlipped);
            _gamePhysics.PlayerIntersectsWithEnemy();
            _gamePhysics.EnemyBoundaries(enemyColliders);
            _gamePhysics.PlayerIntersectWithHumans();
            _gamePhysics.ClimbLadder();
        }

        public void AnimationService(Player player , GameTime gameTime)
        {
            _animationManager.itemsAnimation();
            _animationManager.enemyAnimation();
            _animationManager.HumanAnimation();
            _animationManager.playerAnimationIdle(player, Globals.Content);
            _animationManager.injectAnimation(gameTime);
        }
    }
}
