# 🏎️ Racing Game with ML Agents
As per [Presentation.pdf](https://github.com/user-attachments/files/19106167/Presentation.pdf)

## 📚 Project Presentation  
**Course:** 3D Vision and Extended Reality (2023-2024)  
**University:** Università degli Studi di Padova  
**Presented By:**  
- Alessandro Canel  
- Fateme Baghaei Saryazdi  

---

## 🛣️ Racing Circuit Overview

The racing game features a fully designed 3D circuit with:

- **Checkpoints**: To guide and measure agent progress.
- **Track**: Curved and challenging paths.
- **Shape**: Optimized for training reinforcement learning agents.

---

## 🚗 The Kart - Core Mechanics

### Handling
- Responsive physics for driving control.
- Fine-tuned for learning and performance.

### 3D RayCasting
- Real-time vision system for environment perception.
- Allows the agent to "see" the track and obstacles.

### Vision System
- Uses raycast data as sensor input for training.
- Helps agents navigate using track boundaries and checkpoints.

---

## 🧠 Training & Configuration

### Training Specifications
- Configured through Unity ML-Agents toolkit.
- YAML files define hyperparameters, environment setup, and reward structure.

---

## 🤖 Unity Machine Learning Agents (ML-Agents)

### Definition
- **Open-source toolkit for Unity**.
- Enables games and simulations to become environments for training intelligent agents.
- Supports **3D environments** with complex interactions.

### Learning Methods
- **Reinforcement Learning (RL)**
- **Imitation Learning**

### Core Components
- **Agents**: Autonomous drivers.
- **Environment**: 3D track and physics-based interactions.
- **Brains**: Decision-making algorithms.
- **Rewards**: Feedback guiding behavior.

---

## 🔁 Reinforcement Learning

### What is RL?
- Agents interact with the environment, perform actions, and learn through **trial and error**.
- Agents receive **positive or negative rewards** as feedback.

### Goal
- Learn a policy that **maximizes cumulative rewards** over time.
- Balance short-term gains with long-term strategy.

### Methods
- **Policy-based methods**
- **Value-based methods**

---

## 🏅 Reward System

### Purpose
- Guides the agent towards desired behavior using reward signals.
- Helps the agent distinguish between successful and unsuccessful actions.

### Positive Rewards
✅ Progressing along the track  
✅ Completing laps  
✅ Maintaining high speed  

### Negative Rewards
❌ Collisions  
❌ Driving in reverse  
❌ Idle time  

### Shaping Rewards
- Intermediate rewards for small achievements (like reaching checkpoints) to smooth the learning curve.

---

## 🧑‍🏫 Imitation Learning

### Definition
- Agents learn by **mimicking expert demonstrations**.
- Primary goal is to replicate the expert's behavior in similar scenarios.

### Techniques
- **Generative Adversarial Imitation Learning (GAIL)**
- **Behavioral Cloning**

### Demonstration Recorder
- Enabled to **record expert gameplay**, which serves as training data for the agent.

---

## 🙏 Thank You!

This project demonstrates the potential of combining **3D racing environments** with **cutting-edge machine learning techniques** to create intelligent, self-learning drivers.
