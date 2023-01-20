/*
 * File Pre-Req     :     jquery.js
 * File Desc        :     This file was created to include all javascript basic functions for ease
 * File Create Date :     November,28 2014
 * File Modifi Date : (1) Specify modification date here
 * File Modifi Dtls : (1) Specify modification dtls here
 * File Modifi Prsn : (1) Specify ua name here [Modifier]
 */

// Checks if number is a valid number
// Author : Joel Coehoorn Jan 27 2014 at 20:25
function isNumeric(input) {
    return (input - 0) == input && ('' + input).replace(/^\s+|\s+$/g, "").length > 0;
}

// replaces all the occurences of specified text
function replaceAll(origText, searchText, ReaplaceText) {
    return origText.split(searchText).join(ReaplaceText);
}

// creates repeating string
function repeateWordAndCreate(WordToReapeat, NumberOfRepeatation) {
    return Array(NumberOfRepeatation + 1).join(WordToReapeat); // +1 to match number of repeatation expected
}

function togglePassword()
{
}