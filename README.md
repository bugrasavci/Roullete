
# **Deterministic Roulette - Unity Prototype**

## **Project Overview**
This project is a 3D single-player Unity prototype of a **Deterministic Roulette** game. The player can manually choose the next winning number, simulating controlled gameplay scenarios, while also allowing random number generation. The game includes animations, statistical tracking, and standard roulette rules.

### **Key Features:**
- **Roulette Wheel & Animations:** Realistic spinning wheel and ball drop animation, with a deterministic outcome feature.
- **Player Statistics:** Tracks win/loss count, total profit/loss, and displays them in the UI.
- **Full Roulette Rules & Mechanics:** Standard roulette gameplay, including inside and outside bets, betting chips, and payout calculations.
- **Bet Types Supported:** Straight, Split, Street, Corner, Six Line, Red/Black, Even/Odd, High/Low, Dozens, Columns.

---

## **Installation & Setup**

### **Requirements:**
- Unity 2022.3.x or later
- C# programming knowledge
- Version control system (Git) for repository management

### **Getting Started:**
1. Clone or download the project repository:
   ```
   git clone https://github.com/bugrasavci/Roullete.git
   ```
2. Open the project in Unity.
3. Set the target platform to **PC, Mac & Linux Standalone**.
4. Build and run the game within Unity.

---

## **Gameplay Instructions**

### **Controls:**
- **UI Interaction:** 
  - Click on the chip denominations to place bets on the roulette table.
  - Use the UI to select the deterministic outcome or leave it to random selection.
  - Press **Spin** to start the roulette wheel.
  
- **Bet Placement:**
  - Select from inside or outside bet types.
  - Drag chips onto the roulette table to place your bets.
  
- **Spin Mechanics:**
  - Once the wheel spins, the ball will drop, and the winning number will be highlighted.
  
- **Winning Calculation:**
  - Bets placed on winning numbers will be rewarded based on the type of bet.
  - A payout table is used to calculate winnings after each spin.

---

## **Known Issues or Future Improvements**

- **Resume Feature (Optional):**
  - The game does not currently support resuming from the last saved state upon reopening. This feature is planned for future development.

- **Zero/Double-Zero Option (Optional):**
  - The American Roulette (with double-zero) and European Roulette (with single-zero) selection feature is not yet implemented. The game currently defaults to a single-zero roulette wheel.

---

## **Technologies Used**

- **Engine:** Unity (2022.3.x)
- **Programming Language:** C#
- **Version Control:** Git
- **UI Framework:** Unity’s built-in UI system (No third-party plugins like DoTween were used)

---

## **Future Plans**

1. Implement the **Resume** feature to allow players to continue from their last saved state.
2. Add support for **American Roulette** with double-zero and a toggle option to choose between European and American rules.
3. Improve UI/UX for smoother transitions and better feedback during gameplay.
4. Optimize the spinning wheel animations for even smoother transitions.

---

## **Video Demo**


https://github.com/user-attachments/assets/9b0ed71a-5e1a-4e6f-b27d-f5e83adabe8b

---

## **Licenses and Credits**

- **3D Models & Assets:**
  - All 3D assets (e.g., roulette table, chips, environment) are sourced from the [Unity Asset Store](https://assetstore.unity.com) or publicly available online resources with proper licensing.
  
- **Audio and Sound Effects:**
  - All audio files are sourced from open-source libraries or purchased from authorized platforms.
