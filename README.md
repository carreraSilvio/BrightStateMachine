# Bright FSMs

## FSM
### Features
* States: Have separate states to ease single responsability
* Transitions: Change from one state to the other
* OnEnter/OnExit: Run logic when entering and exiting a state
* Composite States: 
	* States that can act as parents to other states
	* Share their transitions with it's children states
	* Allows nesting of multiple levels

## Pushdown FSM
### Features
* All previous features plus the one bellow
* History: Push states on top of another allowing you to go back to previous state

## LayeredFSM (tbd)
### Features
* Layers: Each layer consists of an independent FSM
* Parallelism: FSMs will run in parallel
* Broadcasting: FSMs can fire events that are broadcasted inside the LayeredFSM.

## Prerequisites
Unity 2018.3 and up

## Install

### Unity 2019.3
1. Open the package manager and point to the repo URL

![Imgur](https://i.imgur.com/iYGgINz.png)

### Before Unity 2019.3

#### Option A
1. Open the manifest
2. Add the repo URL either via https or ssh

#### Option B
1. Clone or download the project zip
2. Copy the repo there inside your project's Assets folder