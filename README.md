<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Empire of Heroes - Interactive README</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            color: #333;
            overflow-x: hidden;
        }
        .hero-section {
            background: linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%);
            color: white;
            padding: 60px 20px;
            text-align: center;
            position: relative;
            overflow: hidden;
        }
        .hero-section::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="100" height="100" viewBox="0 0 100 100"><circle cx="50" cy="50" r="1" fill="white"/></svg>');
            background-size: 2px 2px;
            opacity: 0.3;
            animation: twinkle 3s ease-in-out infinite alternate;
        }
        @keyframes twinkle {
            0% { opacity: 0.3; }
            100% { opacity: 0.8; }
        }
        .hero-content {
            position: relative;
            z-index: 1;
        }
        .hero-title {
            font-size: 4rem;
            font-weight: bold;
            margin-bottom: 20px;
            text-shadow: 2px 2px 4px rgba(0,0,0,0.5);
            animation: glow 2s ease-in-out infinite alternate;
        }
        @keyframes glow {
            from { text-shadow: 2px 2px 4px rgba(0,0,0,0.5), 0 0 20px rgba(102, 126, 234, 0.3); }
            to { text-shadow: 2px 2px 4px rgba(0,0,0,0.5), 0 0 30px rgba(102, 126, 234, 0.6); }
        }
        .hero-subtitle {
            font-size: 1.5rem;
            margin-bottom: 30px;
            opacity: 0.9;
        }
        .game-token {
            display: inline-flex;
            align-items: center;
            background: linear-gradient(45deg, #ffd700, #ffed4e);
            color: #333;
            padding: 8px 16px;
            border-radius: 20px;
            font-weight: bold;
            margin: 0 5px;
            box-shadow: 0 2px 10px rgba(255, 215, 0, 0.3);
            animation: pulse 2s infinite;
        }
        @keyframes pulse {
            0%, 100% { transform: scale(1); }
            50% { transform: scale(1.05); }
        }
        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 40px 20px;
        }
        .nav-tabs {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 30px;
            justify-content: center;
            background: rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(10px);
            padding: 20px;
            border-radius: 15px;
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
        }
        .nav-tab {
            background: linear-gradient(135deg, #667eea, #764ba2);
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 25px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
        }
        .nav-tab:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }
        .nav-tab.active {
            background: linear-gradient(135deg, #ff6b6b, #ee5a52);
            transform: scale(1.05);
        }
        .nav-tab::before {
            content: '';
            position: absolute;
            top: 0;
            left: -100%;
            width: 100%;
            height: 100%;
            background: linear-gradient(90deg, transparent, rgba(255,255,255,0.2), transparent);
            transition: left 0.5s;
        }
        .nav-tab:hover::before {
            left: 100%;
        }
        .section {
            display: none;
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(10px);
            padding: 30px;
            border-radius: 20px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            margin-bottom: 30px;
            animation: fadeIn 0.5s ease;
        }
        .section.active {
            display: block;
        }
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        .section h2 {
            color: #1a1a2e;
            font-size: 2.5rem;
            margin-bottom: 20px;
            text-align: center;
            position: relative;
        }
        .section h2::after {
            content: '';
            position: absolute;
            bottom: -10px;
            left: 50%;
            transform: translateX(-50%);
            width: 80px;
            height: 3px;
            background: linear-gradient(135deg, #667eea, #764ba2);
            border-radius: 2px;
        }
        .cards-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 20px;
            margin: 30px 0;
        }
        .card {
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            color: white;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
            transition: all 0.3s ease;
            cursor: pointer;
            position: relative;
            overflow: hidden;
        }
        .card:hover {
            transform: translateY(-5px) scale(1.02);
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
        }
        .card::before {
            content: '';
            position: absolute;
            top: -50%;
            left: -50%;
            width: 200%;
            height: 200%;
            background: radial-gradient(circle, rgba(255,255,255,0.1) 0%, transparent 70%);
            transform: scale(0);
            transition: transform 0.5s ease;
        }
        .card:hover::before {
            transform: scale(1);
        }
        .card h3 {
            font-size: 1.5rem;
            margin-bottom: 15px;
            position: relative;
            z-index: 1;
        }
        .card p {
            line-height: 1.6;
            position: relative;
            z-index: 1;
        }
        .player-types {
            display: flex;
            gap: 20px;
            margin: 30px 0;
        }
        .player-type {
            flex: 1;
            padding: 25px;
            border-radius: 15px;
            text-align: center;
            transition: all 0.3s ease;
            cursor: pointer;
            position: relative;
            overflow: hidden;
        }
        .web2-player {
            background: linear-gradient(135deg, #74b9ff, #0984e3);
            color: white;
        }
        .web3-player {
            background: linear-gradient(135deg, #a29bfe, #6c5ce7);
            color: white;
        }
        .player-type:hover {
            transform: scale(1.05);
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
        }
        .resources-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
            gap: 15px;
            margin: 20px 0;
        }
        .resource-item {
            background: linear-gradient(135deg, #00b894, #00a085);
            color: white;
            padding: 20px;
            border-radius: 10px;
            text-align: center;
            font-weight: bold;
            transition: all 0.3s ease;
            cursor: pointer;
        }
        .resource-item:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }
        .buildings-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 15px;
            margin: 20px 0;
        }
        .building-item {
            background: linear-gradient(135deg, #fd79a8, #e84393);
            color: white;
            padding: 15px;
            border-radius: 10px;
            text-align: center;
            font-weight: bold;
            transition: all 0.3s ease;
            cursor: pointer;
            position: relative;
        }
        .building-item:hover {
            transform: scale(1.05);
            box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }
        .progress-bar {
            width: 100%;
            height: 20px;
            background: rgba(255,255,255,0.2);
            border-radius: 10px;
            overflow: hidden;
            margin: 20px 0;
        }
        .progress-fill {
            height: 100%;
            background: linear-gradient(90deg, #00b894, #00a085);
            border-radius: 10px;
            transition: width 2s ease;
        }
        .interactive-map {
            background: linear-gradient(135deg, #2d3436, #636e72);
            color: white;
            padding: 30px;
            border-radius: 15px;
            text-align: center;
            margin: 20px 0;
            position: relative;
            overflow: hidden;
        }
        .map-layer {
            display: inline-block;
            margin: 10px;
            padding: 15px 25px;
            background: rgba(255,255,255,0.1);
            border-radius: 25px;
            cursor: pointer;
            transition: all 0.3s ease;
            border: 2px solid transparent;
        }
        .map-layer:hover {
            background: rgba(255,255,255,0.2);
            border-color: #74b9ff;
            transform: scale(1.1);
        }
        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin: 30px 0;
        }
        .stat-card {
            background: linear-gradient(135deg, #fdcb6e, #e17055);
            color: white;
            padding: 25px;
            border-radius: 15px;
            text-align: center;
            transition: all 0.3s ease;
            cursor: pointer;
        }
        .stat-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
        }
        .stat-number {
            font-size: 2.5rem;
            font-weight: bold;
            display: block;
            margin-bottom: 10px;
        }
        .floating-elements {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            pointer-events: none;
            z-index: -1;
        }
        .floating-element {
            position: absolute;
            opacity: 0.1;
            animation: float 6s ease-in-out infinite;
        }
        @keyframes float {
            0%, 100% { transform: translateY(0px) rotate(0deg); }
            50% { transform: translateY(-20px) rotate(180deg); }
        }
        .scroll-indicator {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 4px;
            background: rgba(255,255,255,0.2);
            z-index: 1000;
        }
        .scroll-progress {
            height: 100%;
            background: linear-gradient(90deg, #667eea, #764ba2);
            transition: width 0.1s ease;
        }
        @media (max-width: 768px) {
            .hero-title { font-size: 2.5rem; }
            .nav-tabs { flex-direction: column; align-items: center; }
            .player-types { flex-direction: column; }
            .section { padding: 20px; }
        }
    </style>
</head>
<body>
    <div class="scroll-indicator">
        <div class="scroll-progress" id="scrollProgress"></div>
    </div>

    <div class="floating-elements" id="floatingElements"></div>

    <section class="hero-section">
        <div class="hero-content">
            <h1 class="hero-title">⚔️ Empire of Heroes ⚔️</h1>
            <p class="hero-subtitle">A Dynamic 2D Strategy & Management Game</p>
            <p>Conquer territories, forge alliances, and dominate the world to accumulate the most <span class="game-token">💰 Game Tokens (GT)</span>!</p>
        </div>
    </section>

    <div class="container">
        <div class="nav-tabs">
            <button class="nav-tab active" data-section="overview">🏰 Game Overview</button>
            <button class="nav-tab" data-section="gameplay">🎮 Gameplay Mechanics</button>
            <button class="nav-tab" data-section="players">👥 Player Types</button>
            <button class="nav-tab" data-section="world">🗺️ World & Resources</button>
            <button class="nav-tab" data-section="combat">⚔️ Warriors & Combat</button>
            <button class="nav-tab" data-section="economy">💰 Trade & Economy</button>
            <button class="nav-tab" data-section="mvp">🚀 Current MVP</button>
        </div>

        <section id="overview" class="section active">
            <h2>🏰 Game Overview</h2>
            <div class="cards-grid">
                <div class="card">
                    <h3>🌍 Five Unique Views</h3>
                    <p>Experience the world through Open World exploration, Fighting scenarios, Management Dashboard, Regional overview, and Country-wide perspective.</p>
                </div>
                <div class="card">
                    <h3>🎯 Primary Objective</h3>
                    <p>Acquire the maximum amount of Game Tokens (GT) by building your empire, conquering territories, and forming strategic alliances.</p>
                </div>
                <div class="card">
                    <h3>📱 Mobile-First Design</h3>
                    <p>Built for mobile gameplay, start with a humble village and a single warrior, then expand into a formidable kingdom.</p>
                </div>
            </div>
            
            <div class="stats-grid">
                <div class="stat-card">
                    <span class="stat-number">5</span>
                    <p>Game Views</p>
                </div>
                <div class="stat-card">
                    <span class="stat-number">2000</span>
                    <p>Blocks per Country</p>
                </div>
                <div class="stat-card">
                    <span class="stat-number">30+</span>
                    <p>Building Types</p>
                </div>
                <div class="stat-card">
                    <span class="stat-number">∞</span>
                    <p>Strategic Possibilities</p>
                </div>
            </div>
        </section>

        <section id="gameplay" class="section">
            <h2>🎮 Gameplay Mechanics</h2>
            <div class="interactive-map">
                <h3>🗺️ Explore Different Map Layers</h3>
                <div class="map-layer" data-layer="Open World">Open World</div>
                <div class="map-layer" data-layer="Fighting">Fighting</div>
                <div class="map-layer" data-layer="Management">Management</div>
                <div class="map-layer" data-layer="Regional">Regional</div>
                <div class="map-layer" data-layer="Country">Country</div>
            </div>
            
            <div class="cards-grid">
                <div class="card">
                    <h3>🏰 City Building</h3>
                    <p>Establish multiple cities with Village Halls, Markets, and four custom buildings of your choice. Each city is your strategic stronghold.</p>
                </div>
                <div class="card">
                    <h3>🛣️ Infrastructure</h3>
                    <p>Build roads to accelerate goods and fighter movement. Strategic road placement can give you significant advantages.</p>
                </div>
                <div class="card">
                    <h3>⚔️ PVP Combat</h3>
                    <p>Attack other villages, conquer territories, and engage in epic battles. Strategy and warrior strength determine victory.</p>
                </div>
                <div class="card">
                    <h3>🏛️ Dungeons & PVE</h3>
                    <p>Explore Pokémon-style dungeons inspired by Dofus. Discover unique resources and face challenging NPCs.</p>
                </div>
            </div>
        </section>

        <section id="players" class="section">
            <h2>👥 Player Types</h2>
            <div class="player-types">
                <div class="player-type web2-player">
                    <h3>🌐 Web2 Players</h3>
                    <p>Signup: Email & Password</p>
                    <p>Starting Resources:</p>
                    <ul style="text-align: left; margin-top: 10px;">
                        <li>Game Tokens (GT)</li>
                        <li>Wood & Stone</li>
                        <li>2-3 Houses (2 NPCs each)</li>
                    </ul>
                </div>
                <div class="player-type web3-player">
                    <h3>🔗 Web3 Players</h3>
                    <p>Special Access: Management Dashboard</p>
                    <p>Capabilities:</p>
                    <ul style="text-align: left; margin-top: 10px;">
                        <li>Country-level management</li>
                        <li>Advanced economic controls</li>
                        <li>Enhanced trading features</li>
                    </ul>
                </div>
            </div>
        </section>

        <section id="world" class="section">
            <h2>🗺️ World & Resources</h2>
            <div class="card">
                <h3>🏛️ Countries</h3>
                <p>Equal octagon-shaped territories with 2000 blocks each. Each country has unique raw material distributions.</p>
            </div>
            <div class="card">
                <h3>🌄 Regions</h3>
                <p>Sets of blocks where players establish villages and exploit local resources based on terrain type.</p>
            </div>
            
            <h3>🌿 Raw Materials</h3>
            <div class="resources-grid">
                <div class="resource-item">Wood</div>
                <div class="resource-item">Stone</div>
                <div class="resource-item">Iron</div>
                <div class="resource-item">Gold</div>
                <div class="resource-item">Food</div>
                <div class="resource-item">Gems</div>
            </div>
            
            <h3>🏗️ Buildings & Infrastructure</h3>
            <p>Choose from over 30 different building types to customize your empire!</p>
            <div class="buildings-grid">
                <div class="building-item">Village Hall</div>
                <div class="building-item">Market</div>
                <div class="building-item">Barracks</div>
                <div class="building-item">Farm</div>
                <div class="building-item">Mine</div>
                <div class="building-item">Warehouse</div>
            </div>
        </section>

        <section id="combat" class="section">
            <h2>⚔️ Warriors & Combat</h2>
            <div class="cards-grid">
                <div class="card">
                    <h3>🥷 Warrior NPCs</h3>
                    <p>Each warrior can possess up to 3 unique skills. Train and develop your warriors to create the perfect army composition.</p>
                </div>
                <div class="card">
                    <h3>📈 Experience System</h3>
                    <p>Soldiers gain experience and power through battles. Survivors return stronger, creating a dynamic progression system.</p>
                </div>
                <div class="card">
                    <h3>📊 Combat Stats</h3>
                    <p>Fighting statistics inspired by Pokémon's stat system. Strategy and preparation determine battle outcomes.</p>
                </div>
                <div class="card">
                    <h3>🤝 Alliances & Wars</h3>
                    <p>Form guilds for group inventory, sales, and coordinated warfare. Engage in conflicts between players, villages, and entire countries.</p>
                </div>
            </div>
        </section>

        <section id="economy" class="section">
            <h2>💰 Trade & Economy</h2>
            <div class="cards-grid">
                <div class="card">
                    <h3>🏪 Country Markets</h3>
                    <p>All resources are traded on country-specific markets. Only country owners can facilitate cross-country trades.</p>
                </div>
                <div class="card">
                    <h3>💎 Dual Currency System</h3>
                    <p>Game Token (GT): Primary in-game currency<br>
                    Player Token (PT): Tradeable currency for country ownership</p>
                </div>
                <div class="card">
                    <h3>💼 Country Ownership</h3>
                    <p>Country owners can set taxes on village installation, market transactions, resource production, and road usage.</p>
                </div>
                <div class="card">
                    <h3>📊 Closed Economy</h3>
                    <p>Carefully balanced economic system with management fees to maintain stability and fair gameplay.</p>
                </div>
            </div>
        </section>

        <section id="mvp" class="section">
            <h2>🚀 Current MVP Features</h2>
            <p>MVP Progress: 75% Complete</p>
            <div class="progress-bar">
                <div class="progress-fill" style="width: 75%"></div>
            </div>
            
            <div class="cards-grid">
                <div class="card">
                    <h3>🗺️ World Structure</h3>
                    <p>Two countries with four blocks each (one capital, one village). Each block contains boosted resources.</p>
                </div>
                <div class="card">
                    <h3>👑 Player Roles</h3>
                    <p>One country owner and one free player per country for initial testing and balance.</p>
                </div>
                <div class="card">
                    <h3>⚔️ Combat System</h3>
                    <p>Functional mob fighting in the open world with experience gain and progression mechanics.</p>
                </div>
                <div class="card">
                    <h3>💰 Economy</h3>
                    <p>Four tradeable resources on the market with GT & PT currencies fully implemented.</p>
                </div>
            </div>
        </section>
    </div>

    <script>
        // Tab switching functionality
        const tabs = document.querySelectorAll('.nav-tab');
        const sections = document.querySelectorAll('.section');
        
        tabs.forEach(tab => {
            tab.addEventListener('click', () => {
                const targetSection = tab.dataset.section;
                
                // Remove active class from all tabs and sections
                tabs.forEach(t => t.classList.remove('active'));
                sections.forEach(s => s.classList.remove('active'));
                
                // Add active class to clicked tab and corresponding section
                tab.classList.add('active');
                document.getElementById(targetSection).classList.add('active');
            });
        });

        // Scroll progress indicator
        window.addEventListener('scroll', () => {
            const scrollProgress = document.getElementById('scrollProgress');
            const scrollTop = window.pageYOffset;
            const docHeight = document.body.offsetHeight - window.innerHeight;
            const scrollPercent = (scrollTop / docHeight) * 100;
            scrollProgress.style.width = scrollPercent + '%';
        });

        // Floating elements animation
        function createFloatingElements() {
            const container = document.getElementById('floatingElements');
            const elements = ['⚔️', '🏰', '💰', '🗡️', '🛡️', '👑', '🏹', '🔮'];
            
            for (let i = 0; i < 20; i++) {
                const element = document.createElement('div');
                element.className = 'floating-element';
                element.textContent = elements[Math.floor(Math.random() * elements.length)];
                element.style.left = Math.random() * 100 + '%';
                element.style.top = Math.random() * 100 + '%';
                element.style.animationDelay = Math.random() * 6 + 's';
                element.style.fontSize = (Math.random() * 20 + 20) + 'px';
                container.appendChild(element);
            }
        }
        
        // Interactive map layers
        const mapLayers = document.querySelectorAll('.map-layer');
        mapLayers.forEach(layer => {
            layer.addEventListener('click', () => {
                const layerType = layer.dataset.layer;
                
                // Remove active state from all layers
                mapLayers.forEach(l => l.style.background = 'rgba(255,255,255,0.1)');
                
                // Highlight clicked layer
                layer.style.background = 'rgba(116, 185, 255, 0.3)';
                
                // You could add more interactive functionality here
                console.log(`Exploring ${layerType} layer...`);
            });
        });

        // Add hover effects to cards
        const cards = document.querySelectorAll('.card, .building-item, .resource-item');
        cards.forEach(card => {
            card.addEventListener('mouseenter', () => {
                card.style.transform = 'translateY(-5px) scale(1.02)';
            });
            
            card.addEventListener('mouseleave', () => {
                card.style.transform = '';
            });
        });

        // Initialize floating elements
        createFloatingElements();
    </script>
</body>
</html>
