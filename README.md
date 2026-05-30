# 🥊 Jogo 2D Beat 'em Up

> ⚠️ Projeto em desenvolvimento ativo — funcionalidades podem mudar a qualquer momento.

Um jogo 2D no estilo **Beat 'em Up** clássico desenvolvido em **Unity 6**, com combate lateral, múltiplos inimigos e sistema de vida para jogador e adversários.

---

## 🎮 Sobre o Jogo

Inspirado nos clássicos do gênero Beat 'em Up, o jogador percorre fases avançando pela tela enquanto enfrenta ondas de inimigos em combate corpo a corpo. O projeto está sendo construído com foco em animações fluidas, sistema de dano e uma jogabilidade responsiva.

<img width="1408" height="1018" alt="image" src="https://github.com/user-attachments/assets/c06bae80-6fd4-428a-8231-f27b7c81e7e8" />



---

## 🚧 Status do Desenvolvimento

### ✅ Implementado
- [x] Movimentação do jogador
- [x] Sistema de combate básico
- [x] IA básica de inimigos
- [x] Barra de vida (jogador e inimigo)
- [x] Câmera cinemática (Cinemachine)
- [x] Tileset / Cenário
- [x] Canvas de HUD
- [x] Sistema de eventos

### 🔄 Em Desenvolvimento
- [ ] Mais fases
- [ ] Sistema de pontuação
- [ ] Menu principal e tela de game over
- [ ] Efeitos sonoros e trilha

### ⏳ Pendente
- [ ] Build final

---

## 🛠️ Tecnologias Utilizadas

- **Unity 6** (6000.1.6f1)
- **Universal Render Pipeline (URP)**
- **Cinemachine** — câmera dinâmica e seguimento do jogador
- **Unity Input System** — controles modernos e configuráveis
- **2D Sprite Renderer** com tiling de tileset
- **C#** — scripts de gameplay, IA e sistema de eventos

---

## 📁 Estrutura do Projeto

    Assets/
    ├── Animations/       # Animações dos personagens
    ├── Plugins/          # Plugins externos
    ├── Scenes/           # Cenas do jogo
    ├── Scripts/          # Lógica de gameplay em C#
    └── Settings/         # Configurações do Input System e URP

---

## 📜 Scripts

### ✅ Implementados
- [x] `PlayerHealth.cs` — Sistema de vida do jogador
- [x] `PlayerController.cs` — Controle e movimentação
- [x] `HealthBarUI.cs` — Barra de vida animada na HUD

### ⏳ Pendentes
- [ ] `EnemyHealth.cs` — Vida, dano e morte dos inimigos
- [ ] `EnemyController.cs` — IA de patrulha, detecção e ataque ao jogador
- [ ] `EnemyAnimator.cs` — Controle das animações do inimigo
- [ ] `AttackHitbox.cs` — Colisão dos socos/chutes com hitbox ativa no frame do golpe
- [ ] `ScoreManager.cs` — Pontuação por inimigo derrotado
- [ ] `GameManager.cs` — Fluxo geral: início, game over, vitória, troca de fase
- [ ] `AudioManager.cs` — Efeitos sonoros e trilha sonora
- [ ] `MainMenuController.cs` — Tela inicial e navegação de menus
- [ ] `GameOverScreen.cs` — Tela de game over com opção de reiniciar
- [ ] `LevelLoader.cs` — Carregamento e transição entre fases
- [ ] `EnemySpawner.cs` — Geração de ondas de inimigos por fase

---

## 🔍 Documentação dos Scripts

### `PlayerHealth.cs` — Sistema de Vida do Jogador

Gerencia os pontos de vida do jogador e dispara eventos para atualizar a HUD.

| Método | Descrição |
|---|---|
| `Start()` | Inicializa `currentHealth` com `maxHealth` se estiver zerado e notifica a HUD via evento |
| `Update()` | Detecta alterações manuais feitas no Inspector em tempo real |
| `TakeDamage(int amount)` | Reduz a vida pelo valor informado, dispara `onHealthChanged` e `onDeath` se chegar a zero |
| `Heal(int amount)` | Recupera vida sem ultrapassar o máximo e notifica a HUD |

> **Eventos:** `onHealthChanged(currentHealth, maxHealth)` e `onDeath` — configuráveis direto no Inspector do Unity.

---

### `PlayerController.cs` — Controle e Movimentação do Jogador

Responsável por capturar inputs, mover o personagem, virar o sprite e controlar as animações de combate.

| Método | Descrição |
|---|---|
| `Start()` | Inicializa Rigidbody2D, Animator e velocidade do jogador |
| `Update()` | Chama `PlayerMove()` e `UpdateAnimator()` a cada frame |
| `PlayerMove()` | Lê os eixos de input, controla virada de sprite e detecta os inputs de ataque (X = soco, C = chute) |
| `FixedUpdate()` | Move o Rigidbody2D com `MovePosition` e atualiza o estado `_isWalk` |
| `UpdateAnimator()` | Atualiza o parâmetro `isWalk` no Animator |
| `Flip()` | Inverte a direção do sprite rotacionando 180° no eixo Y |
| `PlayerJab()` | Dispara o trigger `isJab` no Animator (1º e 2º soco do combo) |
| `PlayerPunch()` | Dispara o trigger `isPunch` no Animator (3º soco — golpe final do combo) |
| `PlayerKick()` | Dispara o trigger `isKick` no Animator |
| `PunchController()` *(Coroutine)* | Aguarda 0,75s e reseta o contador do combo se nenhum soco for dado no intervalo |

> **Sistema de combo:** Dois jabs seguidos de um punch (`X, X, X`). O contador reseta automaticamente se o jogador demorar mais de 0,75s entre os socos.

---

### `HealthBarUI.cs` — Barra de Vida com Animação

Controla a barra de vida visual na HUD, com barra verde (vida atual) e barra vermelha (dano com delay animado).

| Método | Descrição |
|---|---|
| `Start()` | Valida as referências e armazena a largura máxima das barras |
| `OnHealthChanged(int current, int max)` | Atualiza a barra verde instantaneamente; a barra vermelha aguarda um delay antes de seguir |
| `Update()` | Move a barra vermelha suavemente em direção à barra verde após o delay |

> **Comportamento visual:** Ao tomar dano, a barra verde cai imediatamente e a vermelha acompanha com atraso animado — efeito clássico de jogos de luta. Na cura, ambas sobem juntas.

---

## 🕹️ Como Rodar o Projeto

**Pré-requisitos**
- [Unity Hub](https://unity.com/download) instalado
- Unity **6000.1.6f1** (ou compatível)

**Passos**

1. Clone o repositório: `git clone https://github.com/DanielRibeir0/Jogo-2D-Beat-em-Up.git`
2. Abra o **Unity Hub** e clique em **Add project from disk**
3. Selecione a pasta clonada
4. Abra a cena `SampleScene` em `Assets/Scenes/`
5. Pressione **Play** no editor para testar

---

## 🤝 Contribuindo

O projeto está em desenvolvimento pessoal, mas sugestões e feedbacks são bem-vindos! Abra uma [issue](https://github.com/DanielRibeir0/Jogo-2D-Beat-em-Up/issues) para reportar bugs ou propor melhorias.

---

## 👨‍💻 Autor

Desenvolvido por **[Daniel Ribeiro](https://github.com/DanielRibeir0)**

---

