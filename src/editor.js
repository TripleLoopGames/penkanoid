'use strict'; // eslint-disable-line

const volkanoidEditor = function execute() {
  let mouseDown = false;
  let clearing = true;
  let currentBlockType = 'ice';
  let currentBlockContentType = 'empty';
  let background = 'basic';
  let wall = 'basic';

  document.addEventListener('mouseup', () => {
    mouseDown = false;
  });

  const wrapperBlock = {
    initialize(element) {
      this.block = element;
      this.setType(currentBlockType, currentBlockContentType);
      this.show();
      this.block.addEventListener('mousedown', (event) => {
        event.preventDefault();
        mouseDown = true;
        clearing = this.visible;
        if (clearing) {
          this.hide();
          return this;
        }
        this.setType(currentBlockType, currentBlockContentType);
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
        this.setType(currentBlockType, currentBlockContentType);
        this.show();
        return this;
      });
      return this;
    },
    setType(type, content) {
      const buildBlockBackground = (blockType, blockContent) => {
        const typeUrl = `url("./images/blocks/${blockType}.png")`;
        if (blockContent === 'empty') {
          return `${typeUrl}`;
        }
        const contentUrl = `url("./images/contents/${blockContent}.png")`;
        return `${contentUrl},${typeUrl}`;
      };
      this.block.style.backgroundImage = buildBlockBackground(type, content);
      this.content = content;
      this.type = type;
      return this;
    },
    show() {
      this.block.style.opacity = 1;
      this.visible = true;
      return this;
    },
    hide() {
      this.block.style.opacity = 0.1;
      this.visible = false;
      return this;
    },
    getBlockData() {
      const row = Number.parseInt(this.block.getAttribute('data-row'), 10);
      const column = Number.parseInt(
        this.block.getAttribute('data-column'),
        10,
      );
      const blockData = {
        row,
        column,
        type: this.type,
      };
      if (this.content === 'empty') {
        return blockData;
      }
      blockData.content = this.content;
      return blockData;
    },
    isVisible() {
      return this.visible;
    },
  };

  const backgroundAndWall = document.querySelector('[class~=level]');

  const backgroundAndWallSelector = document.querySelector(
    '[name~=backgroundAndWallSelect]',
  );

  const setBackgroundAndWall = (back, wally) => {
    backgroundAndWall.style.backgroundImage = `url('./images/walls/${wally}.png'),url('./images/backgrounds/${back}.jpg')`;
    background = back;
    wall = wally;
    return this;
  };

  backgroundAndWallSelector.addEventListener('change', (e) => {
    const type = e.target.value;
    setBackgroundAndWall(type, type);
  });

  const blockContentSelector = document.querySelector(
    '[name~=blockContentSelect]',
  );
  blockContentSelector.addEventListener('change', (e) => {
    // only prize blocks can have content
    if (currentBlockType !== 'prize') {
      blockContentSelector.value = 'empty';
      currentBlockContentType = 'empty';
      return this;
    }
    const content = e.target.value;
    // prize blocks cannot be empty
    if (content === 'empty') {
      blockContentSelector.value = 'star';
      currentBlockContentType = 'star';
      return this;
    }
    currentBlockContentType = content;
    return this;
  });

  const blockSelectors = [
    ...document.querySelectorAll('[data-name~=typeSelector]'),
  ].map((blockSelector) => {
    const type = blockSelector.getAttribute('data-type');
    blockSelector.querySelector('input').addEventListener('change', () => {
      // only prize blocks can have content
      if (type !== 'prize') {
        blockContentSelector.value = 'empty';
        currentBlockContentType = 'empty';
      } else {
        blockContentSelector.value = 'star';
        currentBlockContentType = 'star';
      }
      currentBlockType = type;
    });
    const backPicInfo = blockSelector.querySelector('[class~=blockInfoPic]');
    backPicInfo.style.backgroundImage = `url("./images/blocks/${type}.png")`;
    return blockSelector;
  });

  const blocks = [...document.querySelectorAll('[class~=block]')].map((block) => {
    const myBlock = Object.create(wrapperBlock);
    myBlock.initialize(block);
    return myBlock;
  });

  const links = [...document.querySelectorAll('a')].map((link) => {
    const name = link.getAttribute('data-name');
    if (name === 'save') {
      link.addEventListener('click', () => {
        const toSave = {
          background,
          wall,
          layout: blocks
            .filter(block => block.isVisible())
            .map(block => block.getBlockData()),
        };
        const json = `text/json;charset=utf-8,${encodeURIComponent(
          JSON.stringify(toSave),
        )}`;
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
  };

  holder.ondrop = function (e) {
    this.classList.remove('hover');
    e.preventDefault();

    const file = e.dataTransfer.files[0];
    const reader = new window.FileReader();

    reader.onload = function (event) {
      const setBlocks = (blocksData) => {
        blocks.map(block => block.hide());
        // temp for old maps
        if (blocksData.backgroundAndWall) {
          setBackgroundAndWall(blocksData.backgroundAndWall, blocksData.backgroundAndWall);
        } else {
          setBackgroundAndWall(blocksData.background, blocksData.wall);
        }
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
            console.log('Block not found!');
            return originBlockData;
          }
          const content = originBlockData.content === void 0
            ? 'empty'
            : originBlockData.content;
          foundBlock.setType(originBlockData.type, content);
          foundBlock.show();
          return foundBlock;
        });
      };
      setBlocks(JSON.parse(event.target.result));
    };

    reader.readAsText(file);

    return false;
  };
};

volkanoidEditor();
