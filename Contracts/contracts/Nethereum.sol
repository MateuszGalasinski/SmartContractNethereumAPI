pragma solidity ^0.4.24;

contract Nethereum {

    mapping(address => uint) _balances;
    uint _totalSupply;

    constructor(uint256 initialAmount) public {
        _balances[msg.sender] = initialAmount;
        _totalSupply = initialAmount;
    }

    function transfer(address to, uint256 value) public returns(bool success) {
        require(_balances[msg.sender] >= value, "Not enough tokens");
        _balances[msg.sender] -= value;
        _balances[to] += value;
        emit Transfer(msg.sender, to, value);
        return true;
    }

    event Transfer(address indexed from, address indexed to, uint256 value);

    function balanceOf(address owner) public view returns (uint256 balance) {
        return _balances[owner];
    }
}