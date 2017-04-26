$(document).ready(function () {
    var canvas = document.getElementById("canvas");
    canvas.width= 520;
    canvas.height = 480;
    var jump = false;
    var ctx = canvas.getContext("2d");
    var bgReady = false;
    var bgImage = new Image();
    bgImage.onload = function () {
        bgReady = true;
    };
    bgImage.src = "../img/background2.png";
    var hero = {
        speed: 256, // movement in pixels per second
        x: 0,
        y: 0
    };
    var heroReady = false;
    var heroImage = new Image();
    heroImage.onload = function () {
        heroReady = true;
    };
    heroImage.src = "../img/princess.png";

    var monster = {
        x: 0,
        y: 0,
        velocity: 2
    };
    var monsterReady = false;
    var monsterImage = new Image();
    monsterImage.onload = function () {
        monsterReady = true;
    };
    monsterImage.src = "../img/monster.png";
    var monstersCaught = 0;

    // Handle keyboard controls
    var keysDown = {};

    addEventListener("keydown", function (e) {
        keysDown[e.keyCode] = true;
    }, false);

    addEventListener("keyup", function (e) {
        delete keysDown[e.keyCode];
    }, false);

    addEventListener("keypress", function (e) {
        if (e.keyCode === 32 && jump === false) {
            jump = true;
            var jumpup = setInterval(function () {
                hero.y = hero.y - 16;
                if (hero.y <= 200)
                {
                    var jumpdown = setInterval(function () {
                        hero.y = hero.y + 16;
                        if (hero.y >= 400) {
                            jump = false;
                        clearInterval(jumpdown);
                        }

                    }, 20);
                   
                    clearInterval(jumpup);
      
                }
            }, 20);
            
        }
    })
    // Reset the game when the player catches a monster
    var reset = function () {
        hero.x = 20;
        hero.y = 400;

        // Throw the monster somewhere on the screen randomly
        monster.x = 20 + (Math.random() * (canvas.width - 64));
        monster.y = 420;
    };

    var resetMonster = function () {
        monster.x = hero.x + (Math.random() * (canvas.width - hero.x-30));
        monster.y = 420;
        monster.velocity = Math.random()*4 +1;
    };

    // Update game objects
    var update = function (modifier) {
        monster.x -= monster.velocity;
        if (monster.x <= -50) {
            resetMonster();
        }
        if (37 in keysDown) { // Player holding left
            if (hero.x - hero.speed * modifier >= 5) {
                hero.x -= hero.speed * modifier;
            }
            
        }
        if (39 in keysDown) { // Player holding right
            if (hero.x + hero.speed * modifier <= 440) {
            hero.x += hero.speed * modifier;
            }
        };
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        // Are they touching?
        if (
            hero.x <= (monster.x + 32)
            && monster.x <= (hero.x + 32)
            && hero.y <= (monster.y + 32)
            && monster.y <= (hero.y + 32)
        ) {
            resetMonster();
            ++monstersCaught;
        };
    };

    var render = function () {
        if (bgReady) {
            ctx.drawImage(bgImage, 0, 0);
        }

        if (heroReady) {
            ctx.drawImage(heroImage, hero.x, hero.y);
        }

        if (monsterReady) {
            ctx.drawImage(monsterImage, monster.x, monster.y);
        }

        // Score
        ctx.fillStyle = "rgb(244, 238, 66)";
        ctx.font = "24px Helvetica";
        ctx.textAlign = "left";
        ctx.textBaseline = "top";
        ctx.fillText("Current blood:" + (100 - monstersCaught), 32, 32);
    };

    // The main game loop
    var main = function () {
        var now = Date.now();
        var delta = now - then;

        update(delta / 1000);
        render();

        then = now;

        // Request to do this again ASAP
        requestAnimationFrame(main);
    };

    // Cross-browser support for requestAnimationFrame
    var w = window;
    requestAnimationFrame = w.requestAnimationFrame || w.webkitRequestAnimationFrame || w.msRequestAnimationFrame || w.mozRequestAnimationFrame;

    // Let's play this game!
    var then = Date.now();
    reset();
    main();

});
