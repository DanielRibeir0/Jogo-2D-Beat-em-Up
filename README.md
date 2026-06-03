# 🥊 Jogo 2D Beat 'em Up

> ⚠️ Projeto em desenvolvimento ativo.
>
> Inspirado em clássicos como **Final Fight**, **Streets of Rage**, **Cadillacs & Dinosaurs** e **The Punisher**.
>
> Desenvolvido com **Unity 6**, utilizando **URP**, **C#** e **Cinemachine**.

---

# 🌐 Demo Online

🎮 Jogue agora:

👉 https://danielribeir0.github.io/Jogo-2D-Beat-em-Up/Demo/

---

# 📸 Visão Geral

Um Beat 'em Up 2D clássico onde o jogador enfrenta ondas de inimigos em combate corpo a corpo enquanto avança pelas fases.

### Principais características

- Combate lateral estilo arcade
- Sistema de combos
- Inimigos com IA
- Sistema de vida completo
- HUD animada
- Sistema baseado em eventos
- Estrutura preparada para expansão

---

# 🚧 Status do Projeto

## ✅ Implementado

### 🎮 Jogador

- [x] Movimentação horizontal
- [x] Movimentação vertical
- [x] Flip automático do personagem
- [x] Rigidbody2D
- [x] Sistema de colisão
- [x] Sistema de vida
- [x] Sistema de dano
- [x] Sistema de morte

### 🥊 Combate

- [x] Jab
- [x] Punch
- [x] Kick
- [x] Combo de socos
- [x] Hit Detection
- [x] Dano em inimigos

### 🤖 Inimigos

- [x] IA básica
- [x] Detecção do jogador
- [x] Perseguição
- [x] Controle de distância
- [x] Ataque automático
- [x] Receber dano
- [x] Morte
- [x] Barra de vida própria

### ❤️ Sistema de Vida

- [x] PlayerHealth
- [x] EnemyHealth compartilhado
- [x] Eventos de atualização
- [x] Barra verde
- [x] Barra vermelha animada
- [x] Morte por vida zerada

### 🎨 Interface

- [x] Canvas
- [x] HUD do jogador
- [x] HUD do inimigo
- [x] Barras de vida animadas
- [x] Atualização via eventos

### 🎥 Câmera

- [x] Cinemachine
- [x] Follow Target
- [x] Limites de fase

### 🗺️ Cenário

- [x] Tileset
- [x] Rua principal
- [x] Objetos de cenário
- [x] Barril decorativo

---

## 🔄 Em Desenvolvimento

### 🎯 Gameplay

- [ ] Sistema de pontuação
- [ ] Contador de inimigos derrotados
- [ ] Sistema de waves
- [ ] Respawn de inimigos
- [ ] Progressão da fase

### 🎮 Fluxo do Jogo

- [ ] Game Over
- [ ] Tela de vitória
- [ ] Reiniciar fase
- [ ] Próxima fase

### 🔊 Áudio

- [ ] Sons de soco
- [ ] Sons de chute
- [ ] Sons de dano
- [ ] Sons de morte
- [ ] Música da fase

### 🖥️ Interface

- [ ] Menu Principal
- [ ] Tela de Pause
- [ ] Tela de Vitória
- [ ] Tela de Game Over

---

## 📋 Roadmap

### Fase 1 — Core Gameplay

- [x] Jogador
- [x] Combate
- [x] IA básica
- [x] Sistema de vida
- [x] HUD

### Fase 2 — Progressão

- [ ] Pontuação
- [ ] Respawn
- [ ] Waves
- [ ] Vitória
- [ ] Game Over

### Fase 3 — Polimento

- [ ] Efeitos visuais
- [ ] Sons
- [ ] Música
- [ ] Feedback de impacto

### Fase 4 — Conteúdo

- [ ] Novos inimigos
- [ ] Chefão
- [ ] Itens
- [ ] Armas
- [ ] Segunda fase

---

# 🛠️ Tecnologias Utilizadas

| Tecnologia | Uso |
|------------|------|
| Unity 6 | Engine principal |
| URP | Renderização |
| C# | Programação |
| Cinemachine | Câmera |
| Input System | Controles |
| Sprite Renderer | Renderização 2D |
| Canvas UI | Interface |
| GitHub Pages | Hospedagem da Demo |

---

# 📁 Estrutura do Projeto

```text
Assets
│
├── Animations
│   ├── Player
│   └── Enemy
│
├── Plugins
│
├── Scenes
│   └── SampleScene
│
├── Scripts
│   ├── PlayerController.cs
│   ├── PlayerHealth.cs
│   ├── EnemyController.cs
│   └── HealthBarUI.cs
│
└── Settings
```

---

# 📜 Scripts

## ✅ PlayerController.cs

Responsável por:

- Movimentação
- Leitura dos inputs
- Combo de socos
- Chute
- Flip do personagem
- Aplicação de dano

### Combos

```text
X → Jab

X → Jab

X → Punch
```

### Ataque Alternativo

```text
C → Kick
```

---

## ✅ PlayerHealth.cs

Responsável por:

- Vida máxima
- Vida atual
- Dano
- Cura
- Eventos
- Morte

### Eventos

```csharp
onHealthChanged
onDeath
```

---

## ✅ EnemyController.cs

Responsável por:

- IA do inimigo
- Detecção
- Perseguição
- Distância de ataque
- Ataque automático
- Dano
- Morte

### Estados

```text
Idle

Chase

Attack

Dead
```

---

## ✅ HealthBarUI.cs

Responsável pela HUD.

### Recursos

- Barra verde
- Barra vermelha
- Delay de dano
- Atualização por evento

---

# 🎯 Próximas Funcionalidades

## 🏆 Sistema de Pontuação

```text
+100 por inimigo derrotado
```

Exemplo:

```text
Score: 1200
```

---

## 👊 Sistema de Waves

```text
Wave 1
3 inimigos

Wave 2
5 inimigos

Wave 3
8 inimigos
```

---

## 🚪 Progressão de Fase

```text
Portão fecha

↓

Mata todos os inimigos

↓

Portão abre

↓

Continua
```

---

## 🍔 Itens de Cura

- [ ] Hambúrguer
- [ ] Pizza
- [ ] Frango
- [ ] Refrigerante

---

## 🛢️ Barris Destrutíveis

- [ ] Recebem dano
- [ ] Quebram
- [ ] Soltam itens

---

## ⚔️ Armas

- [ ] Taco
- [ ] Cano
- [ ] Faca
- [ ] Bastão

---

## 👹 Novos Inimigos

### Punk

- [ ] Rápido
- [ ] Pouca vida

### Brutamontes

- [ ] Muito dano
- [ ] Muita vida

### Corredor

- [ ] Ataques rápidos
- [ ] Alta velocidade

---

## 💀 Chefão

Características planejadas:

- [ ] Vida elevada
- [ ] Ataque forte
- [ ] Corrida
- [ ] Golpe especial
- [ ] Segunda fase de combate

---

# 🎮 Como Rodar o Projeto

## Pré-requisitos

- Unity Hub
- Unity 6000.1.6f1

---

## Instalação

Clone o projeto:

```bash
git clone https://github.com/DanielRibeir0/Jogo-2D-Beat-em-Up.git
```

Abra pelo Unity Hub:

```text
Add Project From Disk
```

Selecione a pasta do projeto.

Abra:

```text
Assets/Scenes/SampleScene
```

Clique em:

```text
▶ Play
```

---

# 🤝 Contribuições

Sugestões, melhorias e feedbacks são sempre bem-vindos.

Abra uma **Issue** ou envie um **Pull Request**.

---

# 👨‍💻 Autor

### Daniel Ribeiro

GitHub:

🔗 https://github.com/DanielRibeir0

---

# ⭐ Apoie o Projeto

Se gostou do projeto:

- ⭐ Dê uma estrela no GitHub
- 🐛 Reporte bugs
- 💡 Sugira melhorias

Isso ajuda bastante no desenvolvimento.