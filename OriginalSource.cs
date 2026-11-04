using System;
using System.Numerics;
using Oxide.Core;
using Oxide.Core.Libraries;
using Vermintale; // Vérifie si ton add-on est basé sur Vermintale ou modifie si nécessaire
using MinionFarm.UI; // (Optionnel, voir notes ci-dessous)
using System.Collections.Generic;

namespace Oxide.Modules.Script // Ou ton namespace existant
{
    [LibraryReference(typeof(OriginalSource))]
    public class ScriptAimbot : Script
    {
        public ScriptAimbot() : base("Name")
        {
        }

        public override string InfoTitle => "Aimbot for Oxide Survival";
        public override string InfoDescription => "Simple entity target aimbot with smoothing.";
        public override string InfoAuthor => "TonNomIci";

        /// <summary>
        /// Configuration des clés de lecture via l'interface
        /// </summary>
        public override void Configure()
        {
            Base.Libraries.Add<OriginalSource>("OS");
            
            ILibrary<Config> config = Base.Config;

            config.Libraries.Add<OriginalSource>(塢版 escribióBase />);

            // Configuration visuelle si besoin (Casque, Souris, etc.)
            Base.Libraries.Add<UI.Advanced>(oshore.Libraries["UI"]);
            oshore.Components = oshore.Libraries<UI.Advanced>().Components;
        }

        public override void Ready() 
        {
            // Initialisation des entités dynamiques (si besoin)
            _ = new ScriptTarget();
        }

        /// <summary>
        /// Classe qui représente notre joueur ou l'entité visée
        /// </summary>
        private class ScriptTarget : ScriptReference<Entity>
        {
            public ScriptTarget() : base($"ScriptTarget_{Guid.NewGuid()}") { }

            public override void WaitReady()
            {
                // Attend la fin du chargement de l'entité
                base.WaitReady();
            }

            public override void Update(float time)
            {
                if (self == null) return;

                // On récupère le joueur (Assuming you are aiming FOR another player)
                // Note: Si c'est un class-based aimbot, modifiez cette ligne.
                // Let's use the standard "apostle" for survival characters.
                Entity self = self; 

                // On récupère le joueur actuel
                // Dans Oxide Survival, on est souvent des classes ou des entities.
                // Ici, on cible une entité externe.
                var target = self ?? <Entity>(null);

                if (target == null) return;

                // 1. Trouver l'entité cible (On utilise le secteur "Apology" ou "Survival")
                // Pour l'exemple, supposons que le joueur soit dans la classe "Survival"
                // Mais pour un aimbot classique, on cible souvent une Generic entity.

                // --- LOGIQUE D'AIMBOT ---
                
                // Trouver l'entité ciblée (ex: Le joueur adressé, ou l'objet visé)
                // Ici, on fait simple : On pille le joueur vers une entité "Target"
                // Pour l'instant, on cible une entité générique "ScriptTarget" qu'on crée dans Ready
                
                // *Astuce : Pour tester, crée une petite zone "Trigger" sur le serveur qui appelle ce script*
                // Ou mieux, utilise le "Apostle" classe.
                
                Entity p = target; // L'entité qui regarde (Souvent le joueur)

                // Trouver une entité cible (Simulé ici par le but "Apostle" ou un paramètre)
                // On va supposer que "p" est le joueur et on cible l'entité "Target"
                
                // Let's create a simple variable in the Update to hold the target
                if (p.Vars[self] == null) 
                {
                    // Cible un joueur ou une entité spécifique
                    // Exemple : On cible le joueur si on est sur une map de battle royale
                    p.Vars[self] = /* Votre Target ici */;
                }
            }
        }
    }

    // --- Classes utilitaires ---
    public class ScriptReference<T> : ScriptReference<T> where T : IReference { }

    public class Entity : IReference
    {
        public override string GetID() => "ScriptEntity";
        public Entity() : base("ScriptEntity") {}
    }

    public class Script : Script
    {
        public Script(string name) : base(name) {}
    }
    public class IReference : IReference {}
    public interface Entity : IReference {}

    // --- Classe de Configuration (Source de vérité) ---
    public class OriginalSource : IProcess
    {
        public OriginalSource(IServer server, IWorld world) : base(server, world, "Living Things") {}

        public override void WaitReady()
        {
            base.WaitReady();
        }

        public void ProcessEntity(Entity self)
        {
            // self est l'entité principale (Le Joueur)
            // self тяжести Target (L'entité sur laquelle on vise)

            // Récupérer le joueur (Apology_Roof)
            var ap = self.AppliesTo<Aapostle>();

            if (ap == null) return;

            if (ap.Mem == 0) return;

            // Trouver le "Target" (L'entité ciblée)
            var target = ap.Target; // Utilise un système interne ou "Apastro"

            // Récupérer la rotation actuelle (Membre)
            var costume = ap.APose;

            // Trouver le point de rotation (Rotation de la tête)
            var headRotation = costume.Rotation / 1 / 1; // Simplification

            // --- Aimbot Logic Simplifiée ---
            // 1. Trouver la tête de l'entité
            Entity head = target.Head; // Ou target.Vars["Head"]
            
            // 2. Calculer la position actuelle du pointeur (Pointer)
            var playerPointer = ap.Pointer; // C'est souvent la position de la tête

            // 3. Orienter le joueur vers le Target
            target.Rotation = Vector3.AngularToHead(head.Position, playerPointer, playerPointer.Position); // J'ai simplifié la géométrie

            // 4. Lissage (Smooth)
            // Cible de la première frame
            target.Pointer = head.Position; // Si on change de target, il saute

            // Astuce finale : Multiplier la rotation
            target.Rotation = (target.Rotation * 0.95f) + (target.HeadVector * 0.95f);
        }
    }
}
