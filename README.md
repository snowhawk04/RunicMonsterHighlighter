# RunicMonsterHighlighter

Same idea as the Delirium Spawn highlighter. Unfortunately, all of the monsters and chests in Expedition use the same metadata info. 
I was only interested in large runic monsters which is why I used the scale approach. Set the scale for what objects you want to be tagged. 
Large skulls that spawn runic monsters are upscaled to 1.5. Regular monsters and chest nodes are scaled to 1.0. 
If you want to tag large skulls only, set the value of the slider to something larger than 1.0.

This requires the scale value from the Positioned Component. It's also a really basic implementation, with things on the todo like checking the instance
for an active expedition or only tagging during setup. Could probably dig through and find a way to segregate the various object types (e.g. texture used).

Check out https://github.com/arturino009/ExpeditionIcons for a more feature-filled highlighter for expedition!
