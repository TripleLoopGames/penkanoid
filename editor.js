'use strict'

const volkanoidEditor = function () {
  let mouseDown = false;
  let clearing = true;
  let currentBlockType = 'ice';

  document.addEventListener('mouseup', () => mouseDown = false);

  const wrapperBlock = {
    initialize: function (element) {
      this.block = element;
      this.show();
      this.block.addEventListener('mousedown', (event) => {
        event.preventDefault();
        mouseDown = true;
        clearing = this.visible;
        if (clearing) {
          this.hide();
          return this;
        }
        this.setType(currentBlockType);
        this.show();
        return this;
      });
      this.block.addEventListener('mouseover', () => {
        if (!mouseDown) {
          return this;
        }
        if (clearing) {
          this.hide();
          return this;
        }
        this.setType(currentBlockType);
        this.show();
        return this;
      });
      return this;
    },
    setType: function (type) {
      this.block.style.backgroundImage = `url("./images/blocks/${type}.png")`;
      this.type = type;
      return this;
    },
    show: function () {
      this.block.style.opacity = 1;
      this.visible = true;
      return this;
    },
    hide: function () {
      this.block.style.opacity = 0.1;
      this.visible = false;
      return this;
    },
    getBlockData: function () {
      const row = Number.parseInt(this.block.getAttribute('data-row'), 10);
      const column = Number.parseInt(this.block.getAttribute('data-column'), 10);
      return {
        row,
        column,
        type: this.type
      }
    },
    isVisible: function () {
      return this.visible;
    }
  }

  const radios = [...document.querySelectorAll('input[type=radio][name="block"]')]
    .map(radio => {
      radio.addEventListener('change', () => {
        currentBlockType = radio.value;
      });
    });

  const blockSelectors = [...document.querySelectorAll('[data-name~=typeSelector]')]
    .map(blockSelector => {
      const type = blockSelector.getAttribute('data-type');
      blockSelector.querySelector('input').addEventListener('change', () => {
        currentBlockType = type;
      });
      const backPicInfo = blockSelector.querySelector('[class~=blockInfoPic]');
      backPicInfo.style.backgroundImage = `url("./images/blocks/${type}.png")`;
    });

  const blocks = [...document.querySelectorAll('[class~=block]')]
    .map((block) => {
      const myBlock = Object.create(wrapperBlock);
      myBlock.initialize(block);
      return myBlock;
    });

  const links = [...document.querySelectorAll('a')]
    .map((link) => {
      const name = link.getAttribute('data-name');
      if (name === 'save') {
        link.addEventListener('click', () => {
          const toSave = {
            layout: blocks
              .filter((block) => block.isVisible())
              .map((block) => block.getBlockData())
          }
          const json = "text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(toSave));
          link.href = `data:${json}`;
          link.download = 'level.json';
        });
        return link;
      }
      return link;
    });

  const holder = document.querySelector('[data-name="holder"]');

  if (typeof window.FileReader === void 0) {
    holder.className = 'red';
    holder.textContent = 'File API & FileReader not available';
  } else {
    holder.style.color = 'green';
  }

  holder.ondragover = function () {
    this.classList.add('hover');
    return false;
  };

  holder.ondragend = function () {
    this.classList.remove('hover');
    return false;
  }

  holder.ondrop = function (e) {
    this.classList.remove('hover');
    e.preventDefault();

    const file = e.dataTransfer.files[0];
    const reader = new FileReader();

    reader.onload = function (event) {
      const setBlocks = (blocksData) => {
        blocks.map((block) => block.hide());
        blocksData.layout.map((originBlockData) => {
          const foundBlock = blocks.find((block) => {
            const targetBlockData = block.getBlockData();
            if (targetBlockData.row !== originBlockData.row) {
              return false;
            }
            if (targetBlockData.column !== originBlockData.column) {
              return false;
            }
            return true;
          });
          if (!foundBlock) {
            console.log("Block not found!")
            return originBlockData;
          }
          foundBlock.show();
          return foundBlock;
        })
      }
      setBlocks(JSON.parse(event.target.result));
    };

    reader.readAsText(file);

    return false;
  }
}

volkanoidEditor();
