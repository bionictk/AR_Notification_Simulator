# AR Notification Simulator

### This simulator was built in the context of the following project by Jung Wook Park, Taeheon Kim at Georgia Institute of Technology:

#Designing Contextual Notifications for Augmented Reality

##Introduction
In the future of everyday augmented reality, notifications will be a key part of every application. However, for immersive augmented reality, developers must consider the **context** in which the user is in for a safe and usable design. Currently, this process must be done manually by an experienced UI designer and even then, with the increase of contextual information, this is a tedious job. We introduce ARNode, an authoring toolkit that facilitates this process by giving developers a simple way to create context-aware notifications while still having freedom to fine-tune their designs. Also, we provide a Holographic notification simulator, ARNosi, that allows the developers to test their designs in a fully controllable environment via the Microsoft Hololens.

##Development Environment
###ARNode: AR Notification Designer
1. Operating system: MacOS Sierra
2. IDE: Eclipse Version?
3. Language: Java
4. Library: JavaFX

###ARNosi: Holographic Notification Simulator
1. Operating system: Windows 10
2. IDE
  * Unity Hololens 5.4.0f3-HTP
  * Microsoft Visual Studio Community 2015
3. Languages: C#
4. Libraries
  * UnityEngine
  * Holographic API
5. Target Platform: Microsoft Hololens(Build 10.0.14393.0) 

##Implementation
ARNode provides an easy-to-use authoring method to accelerate context-aware notification design process for AR. It adopts an event-condition-action(ECA) description to handle contextual factors such as location, user activity, cognitive load of the primary activity, emotion, and the time of day.
ARNode is developed based on JavaFX UI library on Eclipse Kepler.  JavaFX enables us to splits service logics with display components through FX Markup Language. Within this benefit, developers will be able to maintain or update its source code or interfaces effectively.

The main user of our system is an AR application developer that targets the Microsoft Hololens. The developer will use ARNode to efficiently create context-aware notifications for his application. Following the ECA flow, the developer first decides the urgency of the notification. ARNode gives suggestions by automatically populating the conditions that the urgency most relates to. After the developer selects the desired conditions, he defines the design(action) of the notification. These design factors can be easily modified afterwards inside Unity while being visualized in 3D. The final output of ARNode is a single script that corresponds to the first selected urgency. The developer repeats this process for each urgency that he wishes to consider in his application.  
Afterwards, in the Unity scene, the developer designs the appearance of the notification. For each script that has been created by ARNode, the developer must create a object and attach the script to that object. When the proper event occurs under certain conditions, the corresponding notification is triggered to be visualized.
ARNosi was created in order to help the final step of the design process. As the conditions are difficult to test in a constrained environment, ARNosi lets the developer test various real-life situations. Using ARNosi, the developer can use the Hololens to test-drive his designed notification while changing the event and conditions of the context.

##Issues
1. Lack of documentation & information  
As the Microsoft Hololens is a fairly new and also quite expensive device, not many developers have been able to start develop on them. Lack of developers means less information in the designated forums compared to other popular platforms. Our solution to this was to proactively participate in the forums and ask questions when we needed to. It would take days for someone to answer them but in the end, every bit was helpful. We also had to go back to the API documentations which does not happen much these days with the abundance of already solved QnA’s on popular platforms.
2. GT Wifi  
For our simulator to be controlled via network, we needed to connect our Hololens to Georgia Tech’s wifi network. Using our credentials, this part was easy. However, setting up the connection between the Hololens and another device was not possible. At the end, we realized that there must be some firewall that was blocking our packets midway. We could not set up our own wireless environment so we got in touch with OIT. The solution they suggested was to use GT Other, which allowed the user to disable the incoming packet security feature. This solution worked and we were finally able to get a connection running.

##UI Contributions
This project provides researchers and programmer a full range of AR notification design factors and pre-defined templates. ARNode forces programmers to consider contextual factors before they begin to design notifications. It also allows them to add their own context by simply modifying lists and categories. Then, ARNode automatically generates a C# script file that can be fully integrated with the Unity development environment. ARNosi equips researchers with simulation environments that they can find optimal design methods in real-world settings. Specifically, they can verify their own contextual design through Wizard of Oz experiments in Unity. 

In this project, we provide three practical design examples that inspire AR researchers to improve the quality of user experience in a safe way. In each example anchor points are determined based on location and user activity. For example, a “roadwork ahead” notification is delivered while a user was driving and his cognitive load was low.

1. UI-based script builder
 1. Provide basic templates and UI components to generate c# scripts for Unity.
 2. Can modify detailed parameters to fit into the purpose of application. 
 3. Reflect current settings on a 3D preview 
2. Context-aware notification
 1. Change a notification view  
3. UI Testing environment
 1. Provides a environment simulator that allows testing in 3D
 2. All environment conditions are controllable to support live testing

##Resources
1. Microsoft Hololens Documentation (http://developer.microsoft.com/en-us/windows/holographic/documentation)
2. Unity Scripting Reference (http://docs.unity3d.com/ScriptReference/)
3. Windows Holographic Development Forum (https://forums.hololens.com/)
4. JavaFX Developer Home (http://www.oracle.com/technetwork/java/javase/overview/javafx-overview-2158620.html)
