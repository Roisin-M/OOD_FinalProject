# **OOD Final Project - Semester 4**

## **Project Overview**

This project was developed as part of the _Object Oriented Development_ module in my 4th semester at **ATU Sligo**. The goal of this project was to create an application that demonstrates a solid understanding of **Object-Oriented Design (OOD)** principles, including:

- Encapsulation
- Inheritance
- Polymorphism
- Abstraction

The project focuses on best practices and modular code.

The application simulates a **Yoga Studio Routine Manager**, allowing users to create, view, and manage custom yoga routines. The application pulls yoga pose data from a JSON file, provides pose details, and lets users add or remove poses from their routines.

---

## **Features**

- **User-friendly interface** for smooth interaction and easy navigation.
- **Object-oriented architecture** with clean and modular code.
- **Pose Management**: Users can browse yoga poses, filter by category, view pose details, and add poses to routines.
- **Routine Management**: Users can create new routines, add poses to routines, and remove poses from routines.
- Implements key design patterns such as:
  - **Singleton** (wherever applicable in resource management).
  - **Factory Pattern** (for pose objects instantiation, if implemented).
  - **Observer** (for routines being updated or modified, if applicable).
- JSON data parsing for dynamic yoga poses.
- Image support to display pose icons.

---

## **Technologies Used**

- **Programming Language**: C#
- **Frameworks/Libraries**: .NET, WPF for UI, `Newtonsoft.Json` for JSON parsing.
- **IDE**: Visual Studio
- **Version Control**: Git

---

## **Usage**

Once the project is running, here's how to use the app:

### **View Yoga Poses:**

1. **Select a category** from the dropdown to filter poses by type (e.g., _Core Yoga_, _Seated Yoga_, etc.).
2. **Click on a pose** from the list to view detailed information, including the pose name, benefits, and a pose icon.
3. Use the **Random** button to view a random pose.

### **Manage Routines:**

1. **Create a new routine** by entering a routine name and clicking the **Add New Routine** button.
2. **Add poses to a routine** by selecting a pose and using the **Add To Routine** button.
3. **View the poses in your routine** by selecting it from the **Routines** list.
4. Navigate through the poses in the routine using the **Previous** and **Next** buttons.
5. **Remove poses from a routine** if necessary.

---

