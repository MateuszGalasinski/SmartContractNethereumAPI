const MyStringStore = artifacts.require("MyStringStore");
const CoinsContract = artifacts.require("CoinsContract");

module.exports = function(deployer) {
  deployer.deploy(MyStringStore);
  deployer.deploy(CoinsContract, 123456789);
};
