import * as coreJs from "core-js/shim";
import {AppRegistry} from 'react-native';
import {Sudoku} from './out/Sudoku';

AppRegistry.registerComponent('Sudoku', () => Sudoku);