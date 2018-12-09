pragma solidity ^0.4.24;

contract MyStringStore {
    string public myString = "Hello World";
    address public owner;

    struct Voter {
        bool isAllowed;
        bool hasVoted;
    }

    struct Candidate {
        bytes32 name;
        uint voteCount;
    }

    struct Ballot {
        bool isActive;
        uint candidatesSize;
        mapping(uint => Candidate) candidates;
        mapping(uint => Voter) voters;  //key means voter id (e.g. 210210)
    }

    Ballot[] public ballots;

    constructor() public {
        owner = msg.sender;

        // rest of the constructor is only example ballot;
        bytes32[] memory candidateNames = new bytes32[](2);
        candidateNames[0] = "ekstrakadnydat"; 
        candidateNames[1] = "lepszykadnydat"; 

        uint[] memory voters = new uint[](1);
        voters[0] = 210183;

        //create new ballot
        ballots.push(Ballot(true, 2));
        Ballot storage createdBallot = ballots[ballots.length - 1];

        for(uint i = 0; i < 2; i++)
        {
            createdBallot.candidates[i] = Candidate({name: candidateNames[i], voteCount: 0});
        }

        for(uint v = 0; v < voters.length; v++)
        {
            createdBallot.voters[voters[v]] = Voter(true, false);
        }
    }

    function set(string memory x) public    {
        myString = x;
    }

    // function getCandidateNamesForBallot(uint ballotIndex) public view returns(bytes32[]){
    //     bytes32[] memory candidateNames = new bytes32[](ballots[ballotIndex].candidatesSize);
    //     for(uint i = 0; i < candidateNames.length; i++)
    //     {
    //         candidateNames[i] = ballots[ballotIndex].candidates[i].name;
    //     }

    //     return candidateNames;
    // }
}