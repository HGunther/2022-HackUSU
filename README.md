# 2022-HackUSU

Many video games these days have arbitrary "difficulty settings" like easy, medium, and hard, that do not really give any indication to a new player about what the experience of that game is going to be.

Leaveraging machine learning, our project attempts to create a dynamic difficulty framework for a game that changes overtime depending on player skill and tries to maximize player engagement.

To do this, we created a simple game in Unity that involves a player at the bottom of the screen who can move right and left and tries to avoid circles that are thrown at it from the top of the screen.

We also employ two neural networks in a GAN inspired strategy. One neural network acts as the player and trains to avoid the obsticals with the lowest energy strategy possible. The other NN, the thrower, decides various parameters of the obsticals such as angle, velocity, and size, and is rewarded the more the player moves without being hit.

This encourages the thrower to launch obstacles at the player in such a way that the player is always moving (and, hence, is engaged) but not so difficult that the player is getting hit.

Though our project is still in the early stages, we can already see our NNs training and adapting in an attempt to get larger and larger rewards. We hope that these NNs will give us more insight into how to craft games that are more fun and engaging for our players.
